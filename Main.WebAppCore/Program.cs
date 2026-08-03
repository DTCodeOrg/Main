using Main.Infrastructure;
using Main.Services;
using Main.WebAppCore.DependentServices;
using Main.WebAppCore.DepententServices;
using Main.WebAppCore.Middleware;
using Microsoft.AspNetCore.HttpOverrides;
using ResourceLibrary.Resources;
using Serilog;

internal class Program
{
    private static async Task Main (string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // --- 2. Core Infrastructure & DI Services ---
        _ = builder.Services.AddHttpContextAccessor ();
        _ = builder.Services.AddDistributedMemoryCache ();
        _ = builder.Services.AddSession (options =>
            {
                // These act as system-wide defaults if the middleware skips a step
                options.IdleTimeout = TimeSpan.FromMinutes (30);
                options.Cookie.HttpOnly = true;
            });

        _ = builder.Services.AddScoped<ITenantContext,TenantContext> ();
        _ = builder.Services.AddScoped<ITenantSetter,TenantSetter> ();

        // --- 1. Logging & Configuration ---
        _ = builder.Host.UseSerilog ();
        _ = builder.AddSerilogConfiguration ();

        // Standard Microsoft generic logger factories configuration
        _ = builder.Services.AddSingleton<Serilog.ILogger> (Serilog.Log.Logger);
        _ = builder.Services.AddLogging (loggingBuilder =>
        {
            _ = loggingBuilder.AddSerilog (dispose: true);
        });

        _ = builder.Services.AddExceptionLogging (builder.Configuration);

        AppSettings.Current = builder.Configuration.GetSection ("MyAppSettings")
            .Get<MyConfigSettings> () ?? new MyConfigSettings ();

        _ = builder.Services.AddDatabase (builder.Configuration);
        _ = builder.Services.AddDatabaseDeveloperPageExceptionFilter ();
        _ = builder.Services.AddRepository (builder.Configuration);
        _ = builder.Services.AddService (builder.Configuration);

        _ = builder.Services.AddAntiforgery ();
        _ = builder.Services.ConfigureOptions<TenantAntiforgeryOptionsSetup> ();

        _ = builder.Services.AddEmailService (builder.Configuration);
        _ = builder.Services.AddCustomLocalization ();

        _ = builder.Services.AddAuthorizations (builder.Configuration);
        _ = builder.Services.AddAuthentication (builder.Configuration);

        _ = builder.Services.AddWebOptimizer (pipeline =>
        {
            _ = pipeline.CompileLessFiles ();
        });

        _ = builder.Services.AddControllers (options =>
        {
            options.Filters.Add (new Microsoft.AspNetCore.Mvc.AutoValidateAntiforgeryTokenAttribute ());
        });

        _ = builder.Services.AddOutputCache ();

        var app = builder.Build();

        // =========================================================================
        // --- 6. HTTP REQUEST PIPELINE EXECUTION ORDER (FULLY RECOGNIZED) ---
        // =========================================================================

        // 1. Core Proxy Headers Mapping
        var forwardedHeadersOptions = new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                         ForwardedHeaders.XForwardedHost |
                         ForwardedHeaders.XForwardedProto
        };
        forwardedHeadersOptions.AllowedHosts.Clear ();

        // FIXED: Clear default network constraints safely to trust loopback Nginx proxy
        forwardedHeadersOptions.KnownNetworks.Clear ();
        forwardedHeadersOptions.KnownProxies.Clear ();

        _ = app.UseForwardedHeaders (forwardedHeadersOptions);

        // 2. Base Diagnostic & Exception Layers
        if ( app.Environment.IsDevelopment () )
        {
            _ = app.UseDeveloperExceptionPage ();
            _ = app.UseMigrationsEndPoint ();
        }
        else
        {
            _ = app.UseGlobalExceptionHandling ();
        }

        _ = app.UseStatusCodePages ();
        _ = app.UseHttpsRedirection ();

        // 3. Static Assets Optimization Compiler
        _ = app.UseWebOptimizer ();
        _ = app.UseStaticFiles ();

        // 4. Multi-Tenant Boundary Identification Routing
        _ = app.UseRouting (); // <-- MUST HAPPEN BEFORE TENANCY RESOLUTION

        // 5. Tenancy Context Extraction
        _ = app.UseMiddleware<TenantResolverHandlingMiddleware> (); // <-- FIXED POSITION

        // 6. Session Management Configuration
        _ = app.UseMiddleware<TenantSessionCookieMiddleware> ();

        _ = app.UseSession ();

        _ = app.UseCustomLocalization ();
        _ = app.UseCors ();

        // 7. Security Authentication Matrix
        _ = app.UseAuthentication ();

        // FIXED POSITION: Validate/Enrich token payloads AFTER base authentication maps the identity
        _ = app.UseMiddleware<TokenValidationMiddleware> ();

        _ = app.UseAuthorization ();

        _ = app.UseAntiforgery ();

        // 9. Data Storage & Output Optimization Pools
        _ = app.UseOutputCache ();

        // 10. Endpoint Mappings
        _ = app.MapControllers ();

        _ = app.MapControllerRoute (
            name: "MyArea",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        _ = app.MapControllerRoute (
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        await app.RunAsync ();

    }
}

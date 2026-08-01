using Main.Infrastructure;
using Main.Services;
using Main.WebAppCore.DependentServices;
using Main.WebAppCore.DepententServices;
using Main.WebAppCore.Middleware;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Options;
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

        // Session & Options Setups Foundations
        _ = builder.Services.AddSession ();
        _ = builder.Services.AddTransient<IConfigureOptions<SessionOptions>,TenantSessionOptionsSetup> ();
        _ = builder.Services.ConfigureOptions<TenantAntiforgeryOptionsSetup> ();
        _ = builder.Services.AddAntiforgery ();

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
        // --- 6. HTTP REQUEST PIPELINE EXECUTION ORDER (FIXED & OPTIMISED) ---
        // =========================================================================

        // 1. Core Proxy Headers Mapping
        var forwardedHeadersOptions = new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                               ForwardedHeaders.XForwardedHost |
                               ForwardedHeaders.XForwardedProto
        };
        forwardedHeadersOptions.KnownNetworks.Add (new Microsoft.AspNetCore.HttpOverrides.IPNetwork (System.Net.IPAddress.IPv6Loopback,0));
        forwardedHeadersOptions.KnownNetworks.Add (new Microsoft.AspNetCore.HttpOverrides.IPNetwork (System.Net.IPAddress.Loopback,0));
        _ = app.UseForwardedHeaders (forwardedHeadersOptions);

        // 2. Base Diagnostic & Exception Layers
        if ( app.Environment.IsDevelopment () )
        {
            _ = app.UseDeveloperExceptionPage (); // Added base developer view for clean stack traces
            _ = app.UseMigrationsEndPoint ();
        }
        else
        {
            _ = app.UseGlobalExceptionHandling ();
        }

        _ = app.UseStatusCodePages ();
        _ = app.UseHttpsRedirection (); // Enforce encryption immediately after error tracking boundaries

        // 3. Static Assets Optimization Compiler
        _ = app.UseWebOptimizer ();
        _ = app.UseStaticFiles ();

        // 4. Multi-Tenant Boundary Identification Routing
        _ = app.UseRouting ();
        _ = app.UseCustomLocalization ();
        _ = app.UseCors ();

        // 5. CRITICAL FIX: Tenancy MUST be evaluated immediately before Authentication runs
        _ = app.UseMiddleware<TenantResolverHandlingMiddleware> ();

        // 6. Security Authentication Matrix
        _ = app.UseAuthentication ();
        _ = app.UseAuthorization ();

        // 7. Data Storage & Output Optimization Pools
        _ = app.UseOutputCache ();
        _ = app.UseSession ();
        _ = app.UseAntiforgery ();

        // 8. Custom Domain Context Validation Rules
        _ = app.UseMiddleware<TenantSecurityMiddleware> ();

        // --- 8. Endpoint Mappings ---
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

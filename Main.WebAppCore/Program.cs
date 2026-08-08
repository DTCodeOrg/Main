using Main.Common;
using Main.Infrastructure;
using Main.Services;
using Main.WebAppCore.DependentServices;
using Main.WebAppCore.Middlewares;
using Microsoft.AspNetCore.HttpOverrides;
using ResourceLibrary.Resources;
using Serilog;

internal class Program
{
    private static async Task Main (string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // --- Core Infrastructure & DI Services ---
        _ = builder.Services.AddHttpContextAccessor ();
        _ = builder.Services.AddDistributedMemoryCache ();

        _ = builder.Services.AddSession (options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes (30);
            options.Cookie.HttpOnly = true;
        });

        _ = builder.Services.AddScoped<ITenantSetter,ResolvedTenantSetter> ();
        _ = builder.Services.AddScoped<IStorageService,LocalStorageService> ();
        _ = builder.Services.AddScoped<ITenantAssetResolver,TenantAssetResolver> ();

        // --- Logging & Configuration ---
        _ = builder.AddSerilogConfiguration ();
        _ = builder.Host.UseSerilog ();
        _ = builder.Services.AddExceptionLoggingMiddleware (builder.Configuration);

        AppSettings.Current = builder.Configuration.GetSection ("MyAppSettings")
            .Get<ConfigurationSettings> () ?? new ConfigurationSettings ();

        _ = builder.Services.AddDatabase (builder.Configuration);
        _ = builder.Services.AddRepository (builder.Configuration);
        _ = builder.Services.AddService (builder.Configuration);
        _ = builder.Services.AddDatabaseDeveloperPageExceptionFilter ();

        _ = builder.Services.AddAntiforgery ();
        _ = builder.Services.ConfigureOptions<TenantAntiforgeryOptionMiddleware> ();

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

        var forwardedHeadersOptions = new ForwardedHeadersOptions
        {
            ForwardedHeaders =
            ForwardedHeaders.XForwardedFor |
            ForwardedHeaders.XForwardedHost |
            ForwardedHeaders.XForwardedProto
        };

        forwardedHeadersOptions.AllowedHosts.Clear ();

        forwardedHeadersOptions.KnownNetworks.Clear ();

        forwardedHeadersOptions.KnownProxies.Clear ();

        _ = app.UseForwardedHeaders (forwardedHeadersOptions);

        if ( app.Environment.IsDevelopment () )
        {
            _ = app.UseDeveloperExceptionPage ();
            _ = app.UseMigrationsEndPoint ();
        }
        else
        {
            _ = app.UseMiddleware<GlobalExceptionHandlingMiddleware> ();
        }

        // 1. Error handling must sit at the absolute top to catch failures down the line
        _ = app.UseStatusCodePages ();
        _ = app.UseHttpsRedirection ();

        // 2. Global CORS policy (Must be evaluated BEFORE static files and routing)
        _ = app.UseCors ();

        // 3. Static Assets Optimization Compiler & Handlers (Bypass tenancy/session overhead)
        _ = app.UseWebOptimizer ();
        _ = app.UseStaticFiles ();

        // 4. Multi-Tenant Boundary Identification Routing
        _ = app.UseRouting ();

        // 5. Tenancy Context Extraction (Saves TenantId to HttpContext.Items)
        _ = app.UseMiddleware<TenantResolverMiddleware> ();

        // 6. Session Management Configuration (Tenant-Scoped Setup)
        _ = app.UseMiddleware<TenantSessionMiddleware> ();
        _ = app.UseSession ();

        // 7. Context Optimization & Processing (Culture needs Tenant context)
        _ = app.UseCustomLocalization ();

        // 8. Security Authentication Matrix (Auth MUST occur BEFORE Antiforgery & Caching)
        _ = app.UseAuthentication ();

        // 9. Custom refresh middleware captures expired requests before authentication evaluations happen
        _ = app.UseMiddleware<TokenRefreshMiddleware> ();
        _ = app.UseMiddleware<TenantValidationMiddleware> ();

        _ = app.UseAuthorization ();

        // 10. Data Storage & Output Optimization Pools
        _ = app.UseAntiforgery ();
        _ = app.UseOutputCache ();

        // 11. Endpoint Mappings
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

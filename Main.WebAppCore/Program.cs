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

        // --- 1. Logging & Configuration ---
        _ = builder.Host.UseSerilog ();
        _ = builder.AddSerilogConfiguration ();

        // 1. Keeps your existing Serilog interface binding active
        _ = builder.Services.AddSingleton<Serilog.ILogger> (Serilog.Log.Logger);

        // 2. FIX: Adds the missing standard Microsoft generic logger factories
        _ = builder.Services.AddLogging (loggingBuilder =>
        {
            _ = loggingBuilder.AddSerilog (dispose: true);
        });

        _ = builder.Services.AddExceptionLogging (builder.Configuration);


        AppSettings.Current = builder.Configuration.GetSection ("MyAppSettings")
            .Get<MyConfigSettings> () ?? new MyConfigSettings ();

        // --- 2. Core Infrastructure & DI Services ---
        _ = builder.Services.AddHttpContextAccessor ();
        _ = builder.Services.AddDistributedMemoryCache ();
        _ = builder.Services.AddScoped<ITenantContext,TenantContext> ();
        _ = builder.Services.AddScoped<ITenantSetter,TenantSetter> ();

        _ = builder.Services.AddDatabase (builder.Configuration);
        _ = builder.Services.AddDatabaseDeveloperPageExceptionFilter ();
        _ = builder.Services.AddRepository (builder.Configuration);
        _ = builder.Services.AddService (builder.Configuration);


        // Inside Program.cs, mirror your isolation architecture for Sessions
        _ = builder.Services.AddSession (); // Register base foundation first
        _ = builder.Services.AddTransient<IConfigureOptions<SessionOptions>,TenantSessionOptionsSetup> ();

        // Inject Options Setup Patches
        _ = builder.Services.ConfigureOptions<TenantAntiforgeryOptionsSetup> ();
        // Enable Core Antiforgery and Session Foundations
        _ = builder.Services.AddAntiforgery ();

        _ = builder.Services.AddEmailService (builder.Configuration);
        _ = builder.Services.AddCustomLocalization ();

        _ = builder.Services.AddAuthorizations (builder.Configuration);
        _ = builder.Services.AddAuthentication (builder.Configuration);

        // --- 4. Web Optimization ---
        _ = builder.Services.AddWebOptimizer (pipeline =>
        {
            _ = pipeline.CompileLessFiles ();
        });

        // Instead, find where you register services (near the top) and update it to this:
        _ = builder.Services.AddControllers (options =>
        {
            options.Filters.Add (new Microsoft.AspNetCore.Mvc.AutoValidateAntiforgeryTokenAttribute ());
        });

        var app = builder.Build();

        // --- 6. HTTP Request Pipeline Execution Order ---

        // 1. FIRST: Safely read Nginx proxy headers
        var forwardedHeadersOptions = new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                       ForwardedHeaders.XForwardedHost |
                       ForwardedHeaders.XForwardedProto
        };

        // Explicitly permit local Nginx loopback traffic without throwing exceptions
        forwardedHeadersOptions.KnownNetworks.Add (new Microsoft.AspNetCore.HttpOverrides.IPNetwork (System.Net.IPAddress.IPv6Loopback,0));
        forwardedHeadersOptions.KnownNetworks.Add (new Microsoft.AspNetCore.HttpOverrides.IPNetwork (System.Net.IPAddress.Loopback,0));

        _ = app.UseForwardedHeaders (forwardedHeadersOptions);


        // 2. SECOND: Process routing-related middlewares
        if ( app.Environment.IsDevelopment () )
        {
            _ = app.UseMigrationsEndPoint ();
        }

        _ = app.UseGlobalExceptionHandling ();

        _ = app.UseStatusCodePages ();

        _ = app.UseWebOptimizer ();

        // 3. THIRD: Resolve tenancy using the freshly parsed proxy Host header
        _ = app.UseMiddleware<TenantResolverHandlingMiddleware> ();

        // 4. FOURTH: Safe to handle HTTPS, Routing, and Static Assets
        _ = app.UseHttpsRedirection (); // Now safely reads X-Forwarded-Proto

        _ = app.UseStaticFiles ();

        _ = app.UseRouting ();

        _ = app.UseCors ();

        _ = app.UseResponseCaching ();

        _ = app.UseCustomLocalization ();

        _ = app.UseSession ();          // 6. Mount isolated session data bucket

        _ = app.UseAntiforgery ();      // 7. Execute Synchronizer Token Pattern validation

        // --- 7. Authentication & Tenant Authorization Defenses ---
        _ = app.UseAuthentication ();

        _ = app.UseAuthorization ();

        // CRITICAL: Runs after Identity sets up User context, allowing you to validate user claims against active tenant contexts
        _ = app.UseMiddleware<TenantSecurityMiddleware> ();

        // --- 8. Endpoint Mappings ---
        _ = app.MapControllers ();

        _ = app.MapControllerRoute (
            name: "MyArea",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        // FIX: Matches: https://finearts.test -> Home/Index
        _ = app.MapControllerRoute (
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        await app.RunAsync ();
    }
}
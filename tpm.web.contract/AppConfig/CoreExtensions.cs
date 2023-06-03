using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;

namespace tpm.web.contract
{
    public static class CoreExtensions
    {
        public static void ConfigAPI(this IServiceCollection services, IConfiguration Configuration)
        {
            AppSetting.Logger = new AppSettingLogger();
            Configuration.Bind("Logger", AppSetting.Logger);

            //services.AddAutoMapper();
            services.AddHttpContextAccessor();
            services.AddMemoryCache();

            //if (AppSetting.Logger.SerilogEnable)
            //{
            //    if (AppSetting.Logger.SerilogDebug) Serilog.Debugging.SelfLog.Enable(Console.Error);
            //    Serilog.Log.Logger = new LoggerConfiguration()
            //        .WriteTo.Seq(AppSetting.Logger.SeqURI)
            //        .CreateLogger();
            //    services.AddSingleton<Core.Log.Interface.ILogger>(new SLogger(Serilog.Log.Logger, AppSetting.Logger.ClientName));
            //}

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(60 * 30);
                options.Cookie.HttpOnly = true;
            });

            services.AddScoped<HttpClient>(c => new HttpClient(new HttpClientHandler()
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
                })
            );

            MemoryCacheOptions memCacheOption = new MemoryCacheOptions();
            //MemoryCacheManager memCache = new MemoryCacheManager(new MemoryCache(memCacheOption));
            //services.AddSingleton<IMemoryCacheManager>(memCache);

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes =
                    ResponseCompressionDefaults.MimeTypes.Concat(
                        new[] { "image/svg+xml" });
            });
            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            services.Configure<FormOptions>(options =>
            {
                options.ValueCountLimit = int.MaxValue;
            });
        }

        public static void ConfigAPI(this IApplicationBuilder app)
        {
            app.UseResponseCompression();
        }
    }
}

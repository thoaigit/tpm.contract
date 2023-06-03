using Microsoft.Extensions.DependencyInjection;
using System;

namespace CoC.MoonSheep.Business
{
    public static class DependencyResolver
    {
        public static IServiceProvider ServiceProvider;

        public static void Configure(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// Create a scope where use this ServiceActivator
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static IServiceScope GetScope(IServiceProvider serviceProvider = null)
        {
            var provider = serviceProvider ?? ServiceProvider;
            return provider?
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
        }
    }
}

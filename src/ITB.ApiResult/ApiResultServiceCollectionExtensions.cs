using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ITB.ApiResultModel
{
    public static class ApiResultSetup
    {
        public static IServiceCollection AddApiResult(this IServiceCollection services, Action<ApiResultOptions> setupAction = null)
        {
            services.TryAddSingleton<ApiResultExecutor>();

            if (setupAction != null)
            {
                services.Configure(setupAction);
            }

            return services;
        }
    }
}

using System;
using ITB.ApiResultModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ITB.ApiResult
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

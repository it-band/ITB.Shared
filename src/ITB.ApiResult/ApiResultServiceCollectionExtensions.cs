using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ITB.ApiResultModel
{
    public static class ApiResultSetup
    {
        public static IServiceCollection AddApiResult(this IServiceCollection services)
        {
            services.TryAddSingleton(typeof(IActionResultExecutor<ApiResult>), typeof(ApiResultExecutor));

            return services;
        }
    }
}

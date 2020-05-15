using System;
using System.Threading.Tasks;
using ITB.ResultModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ITB.ApiResultModel
{
    public class ApiResult : IActionResult
    {
        public object Value { get; set; }

        public int? StatusCode { get; set; }

        public ApiResult(Result value, int? statusCode = null)
        {
            Value = value;
            StatusCode = statusCode;
        }

        public static implicit operator ApiResult(Result value)
        {
            return new ApiResult(value);
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            await context.HttpContext.RequestServices.GetRequiredService<ApiResultExecutor>().ExecuteAsync(context, this);
        }
    }

    public class ApiResultOptions
    {
        public Func<ApiResult, int> StatusCodeAccessor { get; set; } = result => result.StatusCode ?? result.Value.GetStatusCode();
    }

    public class ApiResultExecutor
    {
        private readonly IOptions<ApiResultOptions> _options;

        public ApiResultExecutor(IOptions<ApiResultOptions> options)
        {
            _options = options;
        }

        public virtual async Task ExecuteAsync(ActionContext context, ApiResult result)
        {
            var jsonResult = new JsonResult(result.Value)
            {
                StatusCode = _options.Value.StatusCodeAccessor(result)
            };

            await jsonResult.ExecuteResultAsync(context);
        }
    }
}

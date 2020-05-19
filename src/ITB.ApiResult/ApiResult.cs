using System;
using System.Threading.Tasks;
using ITB.ResultModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace ITB.ApiResultModel
{
    public class ApiResult : ActionResult, IStatusCodeActionResult
    {
        public Result Value { get; set; }

        public int? StatusCode { get; set; }

        public ApiResult(Result value, int? statusCode = null)
        {
            Value = value;
            StatusCode = statusCode ?? value.GetStatusCode();
        }

        public static implicit operator ApiResult(Result value)
        {
            return new ApiResult(value);
        }

        public override async Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            await context.HttpContext.RequestServices.GetRequiredService<IActionResultExecutor<ApiResult>>().ExecuteAsync(context, this);
        }
    }

    public class ApiResultExecutor : IActionResultExecutor<ApiResult>
    {
        public virtual async Task ExecuteAsync(ActionContext context, ApiResult result)
        {
            var jsonResult = new JsonResult(result.Value)
            {
                StatusCode = result.StatusCode
            };

            await jsonResult.ExecuteResultAsync(context);
        }
    }
}

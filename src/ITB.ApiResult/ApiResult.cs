using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ITB.ResultModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

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
        private static readonly string DefaultContentType = new MediaTypeHeaderValue("application/json")
        {
            Encoding = Encoding.UTF8
        }.ToString();

        private readonly JsonOptions _options;
        public ApiResultExecutor(IOptions<JsonOptions> options)
        {
            _options = options.Value;
        }

        public virtual async Task ExecuteAsync(ActionContext context, ApiResult result)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            var jsonSerializerOptions = _options.JsonSerializerOptions;

            var response = context.HttpContext.Response;

            response.ContentType = DefaultContentType;

            if (result.StatusCode != null)
            {
                response.StatusCode = result.StatusCode.Value;
            }

            // Keep this code in sync with SystemTextJsonOutputFormatter
            var responseStream = response.Body;
            await JsonSerializer.SerializeAsync<object>(responseStream, result.Value, jsonSerializerOptions);
            await responseStream.FlushAsync();
        }
    }
}

using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Net;

namespace Questao5.Domain.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public ExceptionMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await ApiExceptionAsync(context, ex, _configuration);
            }
        }

        private static async Task ApiExceptionAsync(HttpContext context, Exception ex, IConfiguration configuration)
        {
            var customError = new ApiException();

            switch (ex)
            {
                case BadRequestException _:
                    {
                        var applicationException = ex as BadRequestException;
                        customError.Message = applicationException?.Mensagem ?? "";
                        customError.TipoError = applicationException?.Tipo ?? "";
                        customError.StatusCode = (int)HttpStatusCode.BadRequest;
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    }

                default:
                    customError.Message = "Ocorreu um erro interno.";
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            Console.WriteLine(ex.ToString());

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(customError, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            })); return;
        }
    }
}

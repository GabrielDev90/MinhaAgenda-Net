using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace MinhaAgenda.Infrastructure.Exceptions.Handler
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (DomainNotFoundException e)
            {
                var error = new CustomError
                {
                    ErrorType = HttpStatusCode.NotFound.ToString(),
                    ErrorMessage = $"{e.Message} {e.InnerException}"
                };
                await SetResponseValuesAsync(error, HttpStatusCode.NotFound, context);

            }
            catch (DomainConflitcException e)
            {
                var error = new CustomError
                {
                    ErrorType = HttpStatusCode.Conflict.ToString(),
                    ErrorMessage = $"{e.Message} {e.InnerException}"
                };
                await SetResponseValuesAsync(error, HttpStatusCode.Conflict, context);

            }
            catch (DomainException e)
            {
                var error = new CustomError
                {
                    ErrorType = HttpStatusCode.BadRequest.ToString(),
                    ErrorMessage = $"{e.Message}"
                };
                await SetResponseValuesAsync(error, HttpStatusCode.BadRequest, context);

            }
            catch (FluentValidation.ValidationException e)
            {
                var erros = JsonConvert.DeserializeObject<List<Validation>>(e.Message);

                var error = new CustomErrorValidation
                {
                    ErrorType = HttpStatusCode.BadRequest.ToString(),
                    ErrorMessage = "Validation Error",
                    Validations = erros
                };
                await SetResponseValidationValuesAsync(error, HttpStatusCode.BadRequest, context);
            }
            catch (System.Exception e)
            {
                var error = new CustomError
                {
                    ErrorType = HttpStatusCode.InternalServerError.ToString(),
                    ErrorMessage = $"{e.Message} {e.InnerException}"
                };
                await SetResponseValuesAsync(error, HttpStatusCode.InternalServerError, context);
            }
        }

        public async Task SetResponseValuesAsync(CustomError customError, HttpStatusCode statusCode, HttpContext context)
        {
            context.Response.StatusCode = (int)statusCode;
            var result = JsonConvert.SerializeObject(customError);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(result);
        }

        public async Task SetResponseValidationValuesAsync(CustomErrorValidation customError, HttpStatusCode statusCode, HttpContext context)
        {
            context.Response.StatusCode = (int)statusCode;
            var result = JsonConvert.SerializeObject(customError);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(result);
        }
    }
}

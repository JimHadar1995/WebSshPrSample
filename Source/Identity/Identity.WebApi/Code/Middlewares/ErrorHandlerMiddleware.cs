using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Core.Exceptions;
using Library.Common.Exceptions;
using Library.Logging.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.WebApi.Code.Middlewares
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandlerMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;

                switch (error)
                {
                    case EntityNotFoundException:
                        response.OnStarting(() =>
                        {

                            return Task.CompletedTask;
                        });
                        response.StatusCode = StatusCodes.Status404NotFound;
                        //await response.WriteAsync("");
                        break;
                    case AuthorizationErrorException authorizationError:
                        {
                            response.StatusCode = StatusCodes.Status403Forbidden;
                            await response.WriteAsync(authorizationError.Message);
                            break;
                        }
                    case BaseException acsEx:
                        {
                            string message = $"{acsEx.Message}";

                            LoggingHelper.Default.Error(acsEx, message);

                            response.StatusCode = StatusCodes.Status400BadRequest;
                            await response.WriteAsync(acsEx.Message);
                            break;
                        }
                    default:
                        await response.WriteAsync(error.Message);
                        break;
                }
            }
        }
    }
}

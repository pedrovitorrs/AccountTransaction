using System;
using System.Net;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Polly.CircuitBreaker;
using Refit;
using AccountTransaction.WebUI.Configuration.Exceptions;
using AccountTransaction.WebUI.Services.Implementation;
using AccountTransaction.WebUI.Services.Interface;

namespace AccountTransaction.WebUI.Configuration.Settings
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private static IAuthService _authService;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IAuthService authService)
        {
            _authService = authService;

            try
            {
                await _next(httpContext);
            }
            catch (CustomHttpRequestException ex)
            {
                await HandleRequestExceptionAsync(httpContext, ex.StatusCode);
            }
            catch (ValidationApiException ex)
            {
                await HandleRequestExceptionAsync(httpContext, ex.StatusCode);
            }
            catch (ApiException ex)
            {
                await HandleRequestExceptionAsync(httpContext, ex.StatusCode);
            }
            catch (BrokenCircuitException)
            {
                HandleCircuitBreakerExceptionAsync(httpContext);
            }
            catch (RpcException ex)
            {
                var statusCode = ex.StatusCode switch
                {
                    //400 Bad Request	    INTERNAL
                    StatusCode.Internal => 400,
                    //401 Unauthorized      UNAUTHENTICATED
                    StatusCode.Unauthenticated => 401,
                    //403 Forbidden         PERMISSION_DENIED
                    StatusCode.PermissionDenied => 403,
                    //404 Not Found         UNIMPLEMENTED
                    StatusCode.Unimplemented => 404,
                    _ => 500
                };

                var httpStatusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), statusCode.ToString());

                await HandleRequestExceptionAsync(httpContext, httpStatusCode);
            }
        }

        private static async Task HandleRequestExceptionAsync(HttpContext context, HttpStatusCode statusCode)
        {
            if (statusCode == HttpStatusCode.Unauthorized)
            {
                if (_authService.ExpiredToken())
                {
                    var tokenRefreshed = await _authService.ValidRefreshToken();
                    if (tokenRefreshed)
                    {
                        context.Response.Redirect(context.Request.Path);
                        return;
                    }
                }

                await _authService.Logout();
                context.Response.Redirect($"/login?ReturnUrl={context.Request.Path}");
                return;
            }

            context.Response.StatusCode = (int)statusCode;
        }

        private static void HandleCircuitBreakerExceptionAsync(HttpContext context)
        {
            context.Response.Redirect("/system-unavailable");
        }
    }
}

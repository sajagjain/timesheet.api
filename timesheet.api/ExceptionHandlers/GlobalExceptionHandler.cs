using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace timesheet.api.ExceptionHandlers
{
    internal class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            ProblemDetails pd = new ProblemDetails()
            {
                Status = httpContext.Response.StatusCode,
                Detail = exception.Message,
                Title = "An error has occured in the system",
            };

            await httpContext.Response.WriteAsJsonAsync(pd);
            return true;
        }
    }
}

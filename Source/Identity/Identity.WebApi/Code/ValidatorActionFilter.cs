using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Identity.WebApi.Code
{
    /// <inheritdoc />
    public class ValidatorActionFilter : IActionFilter
    {
        /// <inheritdoc />
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null || filterContext.ModelState.IsValid) return;
            var errors = filterContext.ModelState.Values.SelectMany(_ => _.Errors.Select(e => e.ErrorMessage)).ToList();
            filterContext.Result = new BadRequestObjectResult(string.Join(", ", errors));
        }

        /// <inheritdoc />
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Sozluk.Api.WebApi.Infrastructure.ActionFilters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(!context.ModelState.IsValid)
            {
               var result= context.ModelState.Values.SelectMany(x => x.Errors)
                                            .Select(x => !string.IsNullOrEmpty(x.ErrorMessage) ?
                                                               x.ErrorMessage : x.Exception?.Message)
                                            .Distinct().ToList();
                context.Result= new BadRequestObjectResult(result);
                return;
            }
            await next();
        }
    }
}

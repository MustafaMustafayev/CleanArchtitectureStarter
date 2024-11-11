using Application.Localization;
using Application.Responses;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebApi.ActionFilters;

public class ModelValidatorActionFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
        {
            return;
        }

        var errors = context.ModelState
                            .Where(ms => ms.Value!.Errors.Any())
                            .ToDictionary(ms => ms.Key, ms => ms.Value?.Errors.Select(e => e.ErrorMessage).ToArray());

        var result = new ErrorDataResult<Dictionary<string, string[]>>(errors!, EMessages.InvalidModel.Translate());
        context.Result = new UnprocessableEntityObjectResult(result);
    }
}
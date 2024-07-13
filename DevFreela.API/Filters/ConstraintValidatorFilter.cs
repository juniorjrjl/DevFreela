using DevFreela.API.Exceptions;
using DevFreela.API.ViewModel;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DevFreela.API.Filters;


public class ConstraintValidatorFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context) {}

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var fieldsDictionary = context.ModelState
                .ToDictionary(ms => ms.Key, ms => ms.Value?.Errors);

            var fields = new List<FieldErrorViewModel>();
            foreach(var entry in fieldsDictionary){
                if (entry.Value is not null)
                {
                    fields.AddRange(entry.Value.Select(ms => new FieldErrorViewModel(entry.Key, ms.ErrorMessage)).ToList());
                }
            }

            throw new FieldErrorException("A requisição contem erros", fields);
        }
    }
}
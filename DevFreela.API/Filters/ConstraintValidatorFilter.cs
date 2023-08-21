using DevFreela.API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DevFreela.API.Filters
{

    public class ConstraintValidatorFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var fieldsDictionary = context.ModelState
                    .ToDictionary(ms => ms.Key, ms => ms.Value?.Errors);

                var fields = new List<FieldErrorDetailsViewModel>();
                foreach(var entry in fieldsDictionary){
                    if (entry.Value is not null)
                    {
                        fields.AddRange(entry.Value.Select(ms => new FieldErrorDetailsViewModel(entry.Key, ms.ErrorMessage)).ToList());
                    }
                }

                var problem = new ProblemViewModel(400, DateTime.Now, "A requisição contem erros", fields);
                context.Result = new BadRequestObjectResult(problem);
            }
        }
    }

}
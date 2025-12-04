using DemoApp.DomainModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DemoApp.Services;

public class ModelStateValidator
{
    public static IActionResult ValidateModelState(ActionContext context)
    {
        (string fieldName, ModelStateEntry entry) = context.ModelState
            .First(x => x.Value.Errors.Count > 0);
        string errorMessage = entry.Errors.First().ErrorMessage;
        
        ErrorEnvelope errorEnvelope = ErrorEnvelope.Error(errorMessage, fieldName);
        var result = new BadRequestObjectResult(errorEnvelope);

        return result;
    }
}

public class ErrorEnvelope
{
    public string ErrorMessage { get; set; }
    public string Field { get; set; }

    public static ErrorEnvelope Error(string error, string fieldName)
    {
        return new ErrorEnvelope
        {
            ErrorMessage = error,
            Field = fieldName
        };
    }
}
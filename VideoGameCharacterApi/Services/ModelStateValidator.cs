using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VideoGameCharacterApi.Services;

public class ModelStateValidator
{
    public static IActionResult ValidateModelState(ActionContext context)
    {
        (string fieldName, ModelStateEntry entry) = context.ModelState
            .First(x => x.Value.Errors.Count > 0);
        string errorSerialized = entry.Errors.First().ErrorMessage;

        Error error = Error.Deserialize(errorSerialized);
        Envelope envelope = Envelope.Error(error, fieldName);
        var result = new BadRequestObjectResult(envelope);

        return result;
    }
}

public class Envelope
{
    public object Result { get; set; }
    public string ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
    public string InvalidField { get; set; }

    public static Envelope Error(Error error, string fieldName)
    {
        return new Envelope
        {
            ErrorCode =  error.Code,
            ErrorMessage = error.Message,
            InvalidField = fieldName
        };
    }
}
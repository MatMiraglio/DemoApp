namespace DemoApp.Models;

public class Result<T>
{
    public string? ErrorMessage { get; }

    private Result(bool isSuccess, T? result, string? errorMessage)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        if (result is T)
        {
            Value = result;
        }
    }
    
    public T? Value { get;  private set; }
    public bool IsSuccess { get; }
    public bool IsFailure { get => !IsSuccess; }

    public static Result<T> Ok(T result)
    {
        return new Result<T>(isSuccess: true, result, null);
    }

    public static Result<T> Fail(string error)
    {
        return new Result<T>(isSuccess: false, default, error);
    }
    
}


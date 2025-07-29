using Shared.Enums;

namespace Shared.Utils;

public class Result
{
    public bool IsSuccess { get; }
    
    public bool IsFailure => !IsSuccess;
    
    public string? ErrorMessage { get; }
    
    public EErrorType? ErrorType { get; }
    
    public List<string>? ValidationErrors { get; }
    
    private const string ValidationExceptionMessage = "Erro de validação";
    
    protected Result(bool isSuccess, string? errorMessage, EErrorType? errorType, List<string>? validationErrors = null)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        ErrorType = errorType;
        ValidationErrors = validationErrors;
    }

    public static Result Success()
    {
        return new Result(true, null, null);
    }
    
    public static Result Fail(string? errorMessage, EErrorType? errorType)
    {
        return new Result(false, errorMessage, errorType);
    }

    public static Result FailValidation(List<string> validationErrors)
    {
        return new Result(false, ValidationExceptionMessage, Enums.EErrorType.Validation, validationErrors);      
    }
    
    public static Result<T> FromError<T>(Result result)
    {
        return Result<T>.Fail(result.ErrorMessage!, result.ErrorType!.Value);
    }
}

public class Result<T> : Result
{
    public T? Value { get;  }
    
    private const string ValidationExceptionMessage = "Erro de validação";

    private Result(T? value, bool isSuccess, string? errorMessage, EErrorType? errorType, List<string>? validationErrors = null)
        : base(isSuccess, errorMessage, errorType, validationErrors)
    {
        Value = value;
    }

    public static Result<T> Success(T data)
    {
        return new Result<T>(data, true, null, null);
    }

    public static Result<T> Fail(string errorMessage, EErrorType eErrorType)
    {
        return new Result<T>(default, false, errorMessage, eErrorType);
    }
        
    public static Result<T> FailValidation(IEnumerable<string> errors)
    {
        return new Result<T>(default, false,ValidationExceptionMessage, Enums.EErrorType.Validation, errors.ToList());
    }
}
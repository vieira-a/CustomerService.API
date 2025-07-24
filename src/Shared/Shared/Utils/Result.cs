using Shared.Enums;

namespace Shared.Utils;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string? ErrorMessage { get; }
    public ErrorType? ErrorType { get; }
    public Dictionary<string, List<string>>? ValidationErrors { get; protected set; }

    protected Result(
        bool isSuccess,
        string? errorMessage,
        ErrorType? errorType,
        Dictionary<string, List<string>>? validationErrors = null
        )
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        ErrorType = errorType;
        ValidationErrors = validationErrors;
    }

    public static Result Success() => new(true, null, null);
    public static Result Fail(string? errorMessage, ErrorType? errorType) => new(false, errorMessage, errorType);
    public static Result FailValidation(Dictionary<string, List<string>> validationErrors) =>
        new(false, "Erro de validação", Enums.ErrorType.Validation, validationErrors);

    public static Result<T> FromError<T>(Result result)
    {
        return Result<T>.Fail(result.ErrorMessage!, result.ErrorType!.Value);
    }
}

public class Result<T> : Result
{
    public T? Value { get; set; }

    private Result(
        T? value,
        bool isSuccess,
        string? errorMessage,
        ErrorType? errorType,
        Dictionary<string, List<string>>? validationErrors = null
        )
        : base(isSuccess, errorMessage, errorType, validationErrors)
    {
        Value = value;
    }

    public static Result<T> Success(T data) => new(data, true, null, null);
    public static Result<T> Fail(string errorMessage, ErrorType errorType) =>
        new(default, false, errorMessage, errorType);
    public new static Result<T> FailValidation(Dictionary<string, List<string>> validationErrors) =>
        new(default, false, "Erro de validação.", Enums.ErrorType.Validation, validationErrors);
}
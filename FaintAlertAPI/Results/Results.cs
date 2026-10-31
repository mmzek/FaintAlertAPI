namespace FaintAlertAPI.Results;

public readonly record struct Error(string Code, string Message)
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public bool IsNone => string.IsNullOrWhiteSpace(Code);
}

public readonly record struct Result
{
    public bool IsSuccess { get; }
    public Error Error { get; }
    private Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }
    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);

    public static Result Combine(params Result[] results) => results.Any(r => !r.IsSuccess)
        ? Failure(new Error("CombinedError", "One or more operations failed."))
        : Success();
}

public readonly record struct Result<T>
{
    public bool IsSuccess { get; }
    public T Value { get; }
    public Error Error { get; }
    private Result(bool isSuccess, T value, Error error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }
    public static Result<T> Success(T value) => new(true, value, Error.None);
    public static Result<T> Failure(Error error) => new(false, default!, error);

    public Result<K> Map<K>(Func<T, K> mapFunc) =>
        IsSuccess ? Result<K>.Success(mapFunc(Value)) : Result<K>.Failure(Error);

    public Result<K> Bind<K>(Func<T, Result<K>> bindFunc) =>
        IsSuccess ? bindFunc(Value) : Result<K>.Failure(Error);
    
    public Result<T> Ensure(Func<T, bool> predicate, Error error) =>
        IsSuccess && !predicate(Value) ? Failure(error) : this;
}
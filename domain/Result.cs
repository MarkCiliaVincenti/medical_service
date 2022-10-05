namespace Domain;

class Result
{
    public bool Success { get; }
    public bool IsFail { get => !Success; }
    public string Error { get; }

    protected Result(bool success, string error)
    {
        Success = success;
        Error = error;
    }

    
    public static Result Ok()
    {
        return new Result(true, string.Empty);
    }

    public static Result Err(string error)
    {
        return new Result(false, error);
    }


    public static Result<T> Ok<T>(T value)
    {
        return new Result<T>(value, true, string.Empty);
    }

    public static Result<E> Err<E>(string error)
    {
        return new Result<E>(default(E), false, error);
    }
}

class Result<T> : Result
{
    public T Value { get; }

    protected internal Result(T value, bool success, string error)
        : base(success, error)
    {
        Value = value;
    }
}
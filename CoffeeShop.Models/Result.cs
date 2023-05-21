namespace CoffeeShop.Models;

public class Result
{
    private readonly List<string> _errors;

    public bool IsSuccessful { get; }
    public int StatusCode { get; }

    internal Result(bool isSuccessful, int statusCode, List<string> errors) 
        => (IsSuccessful, StatusCode, _errors) = (isSuccessful, statusCode, errors);

    public List<string> Errors 
        => IsSuccessful 
        ? new List<string>() 
        : _errors;

    public static Result Success(int statusCode) 
        => new(true, statusCode, new List<string> { });

    public static Result OK 
        => Success(ResultStatusCodes.ResultStatus200OK);

    public static Result NoContent
        => Success(ResultStatusCodes.ResultStatus204NoContent);

    public static Result Failure(int statusCode, IEnumerable<string> errors)
        => new(false, statusCode, errors.ToList());
    
    public static Result Failure(int statusCode, string message)
        => new(false, statusCode, new List<string> { message });
    
    public static Result NotFound(IEnumerable<string> errors)
        => Failure(ResultStatusCodes.ResultStatus404NotFound, errors);

    public static Result NotFound(string error)
        => Failure(ResultStatusCodes.ResultStatus404NotFound, error);

    public static Result NotFound()
        => Failure(ResultStatusCodes.ResultStatus404NotFound, new List<string> { });

    public static Result BadRequest(IEnumerable<string> errors)
        => Failure(ResultStatusCodes.ResultStatus400BadRequest, errors);

    public static Result BadRequest(string error)
        => BadRequest(new List<string> { error });

    public static Result InternalServerError(IEnumerable<string> errors)
        => Failure(ResultStatusCodes.ResultStatus500InternalServerError, errors);

    public static Result InternalServerError(string error)
        => InternalServerError(new List<string> { error });
}

public class Result<TData> : Result
{
    private readonly TData _data;

    private Result(bool isSuccessful, int statusCode, TData data, List<string> errors)
        : base(isSuccessful, statusCode, errors)
        => _data = data;

    public TData Data => IsSuccessful
        ? _data
        : throw new InvalidOperationException(
            $"{nameof(Data)} is not available with failed result. Use {Errors} instead.");

    public static Result<TData> SuccessWith(int statusCode, TData data)
        => new(true, statusCode, data, new List<string> { });

    public static new Result<TData> OK(TData data)
        => SuccessWith(ResultStatusCodes.ResultStatus200OK, data);

    public static Result<TData> Created(TData data)
        => SuccessWith(ResultStatusCodes.ResultStatus201Created, data);

    public static new Result<TData> Failure(int statusCode, IEnumerable<string> errors)
        => new(false, statusCode, default!, errors.ToList());

    public static new Result<TData> Failure(int statusCode, string error)
        => Failure(statusCode, new List<string> { error });

    public static new Result<TData> NotFound()
        => NotFound(new List<string> { });

    public static new Result<TData> NotFound(string error)
        => NotFound(new List<string> { error });

    public static new Result<TData> NotFound(IEnumerable<string> errors)
        => Failure(ResultStatusCodes.ResultStatus404NotFound, errors);

    public static new Result<TData> BadRequest(string error)
        => Failure(ResultStatusCodes.ResultStatus400BadRequest, error);

    public static new Result<TData> InternalServerError(IEnumerable<string> errors)
        => Failure(ResultStatusCodes.ResultStatus500InternalServerError, errors);

    public static new Result<TData> InternalServerError(string error)
        => InternalServerError(new List<string> { error });
}

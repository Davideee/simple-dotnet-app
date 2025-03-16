namespace Common;

public class Result {
    public string? OkMessage { get; protected init; }
    public string? ErrorMessage { get; protected init; }
    public bool IsSuccess { get; protected init; }

    public static Result Ok(string message) {
        return new Result(true, message);
    }

    public static Result Fail(string message) {
        return new Result(false, message);
    }

    protected Result(){}

    protected Result(bool ok, string? message) {
        IsSuccess = ok;
        if (ok) {
            OkMessage = message;
            ErrorMessage = null;
        } else {
            OkMessage = null;
            ErrorMessage = message;
        }
    }

    public ResultDto CreateDto() {
        return new ResultDto(OkMessage, ErrorMessage);
    }
}

public class Result<T> : Result {
    private readonly T? _data;

    private Result(T? data, bool isSuccess, string? message) : base(isSuccess, message) {
        _data = data;
    }

    private Result(T? data, Result result) {
        _data = data;
        IsSuccess = result.IsSuccess;
        OkMessage = result.OkMessage;
        ErrorMessage = result.ErrorMessage;
    }

    public static Result<T> Ok(T data, string message) {
        return new Result<T>(data, true, message);
    }

    public new static Result<T> Fail(string message) {
        return new Result<T>(default, false, message);
    }

    public static Result<T> Ok(T data) {
        return new Result<T>(data, true, null);
    }

    public Result<TDestination> MapData<TDestination>() where TDestination : class, new() {
        return IsSuccess ? new Result<TDestination>(null, this) : new Result<TDestination>(GenericMapper.Map<T, TDestination>(_data), true, null);
    }

    public Result<IList<TDestination>?> MapList<TSource, TDestination>() where TDestination : class, new() {
        if (IsSuccess) {
            IList<TDestination>? mappedData = GenericMapper.MapList<T,TSource, TDestination>(_data);
            return new Result<IList<TDestination>?>(mappedData, true, null);
        }
        return new Result<IList<TDestination>?>(null, this);
    }

    public new ResultDto<T?> CreateDto() {
        return IsSuccess ? new ResultDto<T?>(_data, null) : new ResultDto<T?>(default, ErrorMessage);
    }
}


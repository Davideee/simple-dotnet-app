namespace Common;

public class ResultDto<T> {
    public T? Data { get; set; }
    public string? Error { get; set; }

    public ResultDto(T? data, string? error) {
        Data = data;
        Error = error;
    }
}

public class ResultDto : ResultDto<string> {
    public ResultDto(string? data, string? error) : base(data, error) {
    }
}
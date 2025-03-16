namespace Common; 

public class Error {
    public int ErrorCode { get; init; }
    public string Message { get; init; }

    public Error(int errorCode, string message) {
        ErrorCode = errorCode;
        Message = message;
    }
}
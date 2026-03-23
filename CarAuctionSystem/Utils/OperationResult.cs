namespace CarAuctionSystem.Utils;

public class OperationResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = "";
    
    public List<string> Errors { get; set; } = new List<string>();
    
    public ErrorType ErrorType { get; set; } = ErrorType.None;
}

public class OperationResult<T> : OperationResult
{
    public T? Data { get; set; }  
}
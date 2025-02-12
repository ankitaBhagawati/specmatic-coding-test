namespace Models;
public class ErrorResponseBody
{
    public DateTime Timestamp { get; set; }
     public int Status { get; set; }
    public string? Error { get; set; }
    public string? Path { get; set; }
}
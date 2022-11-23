using StudyBuddy.WebUi.Models;

namespace StudyBuddy.WebUi.Wrappers;

public class Response<T>
{
    public T? Data { get;  set; }
    public int StatusCode { get;  set; }
    public ErrorDto Error { get; set; }
    
    public long? Take { get; set; }
    public int? Page { get; set; }
    public int? TotalPage { get; set; }
    
}
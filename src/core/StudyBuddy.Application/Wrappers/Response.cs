using StudyBuddy.Application.Dtos;

namespace StudyBuddy.Application.Wrappers;

public class Response<T>
{
    public T? Data { get; private set; }
    public int StatusCode { get; private set; }
    public ErrorDto Error { get; private set; }

    public static Response<T> Success(T data, int statusCode)
    {
        return new Response<T> { Data = data, StatusCode = statusCode };
    }

    public static Response<T> Success(int statusCode)
    {
        return new Response<T> { StatusCode = statusCode };
    }

    public static Response<T> Fail(ErrorDto errorDto, int statusCode)
    {
        return new Response<T> { Error = errorDto, StatusCode = statusCode };
    }

    public static Response<T> Fail(string errorMessage, int statusCode)
    {
        var errorDto = new ErrorDto(errorMessage);
        return new Response<T> { Error = errorDto, StatusCode = statusCode };
    }
}
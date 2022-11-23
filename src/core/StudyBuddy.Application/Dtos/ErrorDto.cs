namespace StudyBuddy.Application.Dtos;

public class ErrorDto
{
    public List<string> Errors { get; private set; } = new List<string>();


    public ErrorDto(string error)
    {
        Errors.Add(error);
    }

    public ErrorDto(List<string> errors)
    {
        Errors = errors;
    }
}
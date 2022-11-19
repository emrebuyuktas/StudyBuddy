namespace StudyBuddy.WebUi.Models;

public class ErrorDto
{
    public List<String> Errors { get; private set; } = new List<string>();


    public ErrorDto(string error)
    {
        Errors.Add(error);
    }

    public ErrorDto(List<string> errors)
    {
        Errors = errors;
    }
}
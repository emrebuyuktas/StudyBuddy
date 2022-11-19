namespace StudyBuddy.Application.Utils;

public static class StringExtensions
{
    public static string ToModeratorId(this string classroomId, string userId) => $"{classroomId}-{userId}";
}
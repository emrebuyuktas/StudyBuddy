﻿namespace StudyBuddy.Application.Dtos;

public class MessageDto
{
    public string? UserId { get; set; }
    public string UserName { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
}
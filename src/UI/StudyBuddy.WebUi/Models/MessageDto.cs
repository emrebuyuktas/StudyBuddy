﻿namespace StudyBuddy.WebUi.Models;

public class MessageDto
{
    public string? UserId { get; set; }
    public string UserName { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
}
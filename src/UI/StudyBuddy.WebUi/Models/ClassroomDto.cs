﻿namespace StudyBuddy.WebUi.Models;

public class ClassroomDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<TagDto> Tags { get; set; }
    public List<MessageDto> Messages { get; set; }
    public List<UserDto> AppUsers { get; set; }
}
﻿namespace StudyBuddy.WebUi.Models;

public class SignupDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public List<TagDto> TagDtos { get; set; }
}
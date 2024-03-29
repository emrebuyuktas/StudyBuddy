﻿using System.Text.Json.Serialization;

namespace StudyBuddy.Application.Dtos;

public abstract class BaseDto
{
    public string? AccessToken { get; set; }
    public DateTime? AccessTokenExpiration { get; set; } 
}
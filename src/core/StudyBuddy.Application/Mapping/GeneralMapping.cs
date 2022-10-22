using AutoMapper;
using StudyBuddy.Application.Features.Commands.Auth;
using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Application.Mapping;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<SignupCommand, AppUser>();
    }
}
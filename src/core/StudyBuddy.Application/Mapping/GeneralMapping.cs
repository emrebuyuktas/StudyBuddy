using AutoMapper;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Features.Commands.Auth;
using StudyBuddy.Application.Features.Commands.Classroom;
using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Application.Mapping;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<SignupCommand, AppUser>();
        CreateMap<ClassroomDto, Classroom>().ReverseMap();
        CreateMap<MessageDto, Message>().ReverseMap();
        CreateMap<AppUser, UserDto>().ReverseMap();
        CreateMap<CreateClassroomCommand, Classroom>();
        
        // CreateMap<AppUser,UserClassroomDto>().ForMember(x=>x.ClassroomName, 
        //     y=>y.MapFrom(z=>z.Classrooms))
    }
}
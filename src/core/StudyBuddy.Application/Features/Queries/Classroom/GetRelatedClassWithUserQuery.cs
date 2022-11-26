using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Interfaces;
using StudyBuddy.Application.Utils;
using StudyBuddy.Application.Wrappers;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Entities.MongoDb;

namespace StudyBuddy.Application.Features.Queries.Classroom;

public class GetRelatedClassWithUserQuery : IRequest<Response<List<ClassroomListDto>>>
{
    
}

public class GetRelatedClassWithUserHandler : RequestHandlerBase<GetRelatedClassWithUserQuery, Response<List<ClassroomListDto>>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetRelatedClassWithUserHandler(IHttpContextAccessor httpContextAccessor, IApplicationDbContext dbContext, UserManager<AppUser> userManager, IMapper mapper) : base(httpContextAccessor)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public override async Task<Response<List<ClassroomListDto>>> Handle(GetRelatedClassWithUserQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _dbContext.AppUsers.Where(x => x.Id == UserId).Include(x => x.Classrooms).Include(x => x.Tags)
            .FirstAsync(cancellationToken: cancellationToken);
        var classrooms = _dbContext.Classrooms.Where(x => x.Tags.Any(x => user.Tags.Any(y => y == x)))
            .OrderBy(r => Guid.NewGuid()).Take(10).Select(x => new ClassroomListDto
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Tags = _mapper.Map<List<TagDto>>(x.Tags)
            }).ToList();

        return Response<List<ClassroomListDto>>.Success(classrooms, 200);
    }
}
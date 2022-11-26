using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Interfaces;
using StudyBuddy.Application.Utils;
using StudyBuddy.Application.Wrappers;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Entities.MongoDb;

namespace StudyBuddy.Application.Features.Commands.User;

public class AddTagToUserCommand : IRequest<Response<NoDataDto>>
{
    public AddTagToUserCommand(List<Tags> tagsList)
    {
        TagsList = tagsList;
    }

    public List<Tags> TagsList { get; set; }
}

public class AddTagToUserCommandHandler : RequestHandlerBase<AddTagToUserCommand, Response<NoDataDto>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IApplicationDbContext _dbContext;
    
    public AddTagToUserCommandHandler(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IApplicationDbContext dbContext) : base(httpContextAccessor)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }

    public override async Task<Response<NoDataDto>> Handle(AddTagToUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(UserId);
        if(user is null) return Response<NoDataDto>.Fail("User not found",400);
        var tags = await _dbContext.Tags.Where(x=>request.TagsList.Contains((Tags)x.Id)).ToListAsync(cancellationToken: cancellationToken);
        user.Tags = tags;
        await _dbContext.SaveChangesAsync();
        return Response<NoDataDto>.Success(201);
    }
}
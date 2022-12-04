using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Interfaces;
using StudyBuddy.Application.Utils;
using StudyBuddy.Application.Wrappers;
using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Application.Features.Commands.Message;

public class SaveMessageCommand : IRequest<Response<NoDataDto>>
{
    public string ClassroomId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }

    public SaveMessageCommand(string classroomId, string content, DateTime createdDate)
    {
        ClassroomId = classroomId;
        Content = content;
        CreatedDate = createdDate;
    }
}

public class SaveMessageCommandHandler : RequestHandlerBase<SaveMessageCommand, Response<NoDataDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly UserManager<AppUser> _userManager;
    public SaveMessageCommandHandler(IHttpContextAccessor httpContextAccessor, IApplicationDbContext dbContext, UserManager<AppUser> userManager) : base(httpContextAccessor)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public override async Task<Response<NoDataDto>> Handle(SaveMessageCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(UserId);
        var classroom = await _dbContext.Classrooms.Where(x => x.Id == new Guid(request.ClassroomId)).FirstOrDefaultAsync(cancellationToken: cancellationToken);
        if(user is null || classroom is null) return Response<NoDataDto>.Fail("user or classroom not found",404);
        var message = new StudyBuddy.Domain.Entities.Message
        {
            ClassroomId = classroom.Id,
            Classroom = classroom,
            Content = request.Content,
            CreatedDate = request.CreatedDate,
            User = user,
            UserId = user.Id
        };
        await _dbContext.Messages.AddAsync(message, cancellationToken);
        await _dbContext.SaveChangesAsync();
        return Response<NoDataDto>.Success(201);
    }
}
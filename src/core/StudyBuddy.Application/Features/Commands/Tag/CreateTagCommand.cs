using MediatR;
using Microsoft.AspNetCore.Http;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Helpers;
using StudyBuddy.Application.Interfaces;
using StudyBuddy.Application.Utils;
using StudyBuddy.Application.Wrappers;
using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Application.Features.Commands.Tag;

public class CreateTagCommand : IRequest<Response<NoDataDto>>
{
    
}

public class CreateTagCommandHandler : RequestHandlerBase<CreateTagCommand, Response<NoDataDto>>
{
    private readonly IApplicationDbContext _dbContext;
    public CreateTagCommandHandler(IHttpContextAccessor httpContextAccessor, IApplicationDbContext dbContext) : base(httpContextAccessor)
    {
        _dbContext = dbContext;
    }

    public override async Task<Response<NoDataDto>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var tagList = TagHelper.TagToList();
        foreach (var item in tagList)
        {
            if (!_dbContext.Tags.Any(x => x.Id == item))
            {
                await _dbContext.Tags.AddAsync(new Domain.Entities.Tag
                {
                    Id = item,
                    Name = ((Tags)item).ToString()
                });
            }
        }

        await _dbContext.SaveChangesAsync();
        return Response<NoDataDto>.Success(201);

    }
}
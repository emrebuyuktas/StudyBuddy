using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Utils;
using StudyBuddy.Application.Wrappers;
using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Application.Features.Commands.Auth;

public class SignoutCommand : IRequest<Response<NoDataDto>>
{
    public string UserId { get; set; }
}

public class SignoutCommandHandler : RequestHandlerBase<SignoutCommand, Response<NoDataDto>>
{
    private readonly IDistributedCache _distributedCache;

    public SignoutCommandHandler(IDistributedCache distributedCache,
        IHttpContextAccessor contextAccessor) : base(contextAccessor)
    {
        _distributedCache = distributedCache;
    }

    public override async Task<Response<NoDataDto>> Handle(SignoutCommand request, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync($"id:{request.UserId}", cancellationToken);
        return Response<NoDataDto>.Success(204);
    }
}
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Wrappers;

namespace StudyBuddy.Application.Features.Commands.Auth;

public class DeleteRefreshTokenCommand : IRequest<Response<NoDataDto>>
{
    public string UserId { get; set; }
}

public class DeleteRefreshTokenCommandHandler : IRequestHandler<DeleteRefreshTokenCommand, Response<NoDataDto>>
{
    private readonly IDistributedCache _distributedCache;

    public DeleteRefreshTokenCommandHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task<Response<NoDataDto>> Handle(DeleteRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync($"id:{request.UserId}", cancellationToken);
        return Response<NoDataDto>.Success(204);
    }
}
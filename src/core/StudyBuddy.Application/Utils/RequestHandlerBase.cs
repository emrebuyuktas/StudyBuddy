using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace StudyBuddy.Application.Utils;

public abstract class RequestHandlerBase<T1,T2> : IRequestHandler<T1, T2> where T1 : IRequest<T2>
{
    public string UserId { get; set; }

    public RequestHandlerBase(IHttpContextAccessor httpContextAccessor)
    {
        UserId = httpContextAccessor.HttpContext.User.Claims.First(x => x.Type==ClaimTypes.NameIdentifier).Value;
    }

    public abstract Task<T2> Handle(T1 request, CancellationToken cancellationToken);
}
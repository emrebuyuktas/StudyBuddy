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
//
// public class GetRelatedClassWithUserHandler : RequestHandlerBase<GetRelatedClassWithUserQuery, Response<List<ClassroomListDto>>>
// {
//     private readonly IApplicationDbContext _dbContext;
//     private readonly IMongoDbRepository<ClassroomTag> _classroomTagRepos;
//     private readonly IMongoDbRepository<UserTag> _userTagRepos;
//     private readonly UserManager<AppUser> _userManager;
//
//     public GetRelatedClassWithUserHandler(IHttpContextAccessor httpContextAccessor, IApplicationDbContext dbContext, IMongoDbRepository<ClassroomTag> classroomTagRepos, UserManager<AppUser> userManager, IMongoDbRepository<UserTag> userTagRepos) : base(httpContextAccessor)
//     {
//         _dbContext = dbContext;
//         _classroomTagRepos = classroomTagRepos;
//         _userManager = userManager;
//         _userTagRepos = userTagRepos;
//     }
//
//     public override async Task<Response<List<ClassroomListDto>>> Handle(GetRelatedClassWithUserQuery request, CancellationToken cancellationToken)
//     {
//         var userTagList = await _userTagRepos.GetAsync(UserId);
//         var user = await _dbContext.AppUsers.Where(x=>x.Id==UserId).Include(x=>x.Classrooms).FirstAsync();
//         var classrooms=_dbContext.Classrooms.Where(x => x.Tags.Any(x => user.Tags.Any(y => y == x)))
//             .OrderBy(r=>Guid.NewGuid()).Take(10).Select(x=>new ClassroomListDto
//             {
//                 Id = x.Id.ToString(),
//                 Name = x.Name,
//                 Tags = x.Tags
//             } ).ToList();
//         // var classroomTags = _classroomTagRepos.AsQueryable()
//         //     .Where(x => x.Tags.Any(x => userTagList.Tags.Any(y => y == x)) && !user.Classrooms.Any(y=>y.ClassroomId.ToString()==x.Id))
//         //     .OrderBy(r=>Guid.NewGuid()).Take(10).ToList();
//
//         // var classroomTags = _classroomTagRepos.AsQueryable()
//         //     .Where(o => o.Tags.Any(pn => userTagList.Tags.Contains(pn)))
//         //     .ToList();
//     //     var filter = Builders<ClassroomTag>
//     //         .Filter.Nin(x => x.Id, user.Classrooms.Select(l => l.ClassroomId.ToString()));
//     //     var classroomTags = _classroomTagRepos.AsQueryable()
//     //         .Where(o => o.Tags.Any(pn => userTagList.Tags.Contains(pn)))
//     //         .ToList();
//     //
//     //     var x = _classroomTagRepos.Collection();
//     //     
//     //     var documentCount=await x.CountDocumentsAsync(new BsonDocument());
//     //     var sort = Builders<ClassroomTag>.Sort;
//     //     Random rnd = new Random(Guid.NewGuid().GetHashCode());
//     //     var res= x.Find(filter).Skip((int?)rnd.NextInt64(0,documentCount)).Limit(10).ToList();
//     //     var list = new List<Guid>();
//     //     res.ForEach(x =>
//     //     {
//     //         list.Add(Guid.Parse(x.Id));
//     //     });
//     //     var result = _dbContext.Classrooms.Where(x=>list.Contains(x.Id)).Select(x=>new ClassroomListDto
//     //     {
//     //         Id = x.Id.ToString(),
//     //         Name = x.Name,
//     //         Tags = res.Find(y=>y.Id==x.Id.ToString()).Tags
//     //     }).ToList();
//     //     return Response<List<ClassroomListDto>>.Success(result,200);
//     // }
//     return new Response<List<ClassroomListDto>>();
// }
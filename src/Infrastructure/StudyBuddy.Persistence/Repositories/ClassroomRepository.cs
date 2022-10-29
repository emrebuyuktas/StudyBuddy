using Microsoft.EntityFrameworkCore;
using StudyBuddy.Application.Interfaces.Repositories;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Persistence.Context;

namespace StudyBuddy.Persistence.Repositories;

public class ClassroomRepository : GenericRepository<Classroom>,IClassroomRepository
{
    public DbSet<Domain.Entities.Classroom> DbSet { get; set; }
    public ClassroomRepository(ApplicationDbContext context) : base(context)
    {
        DbSet=base._context.Set<Domain.Entities.Classroom>();
    }

    public async Task<Classroom> GetClassWithUsersAsync(string id)
    {
        return await DbSet.Where(x => x.Id.ToString() == id).Include(x=>x.Messages).Include(x=>x.Tags).
            Include(x => x.Users).ThenInclude(x => x.AppUser).SingleOrDefaultAsync();
    }
}
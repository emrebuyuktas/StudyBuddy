using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Application.Interfaces.Repositories;

public interface IClassroomRepository : IGenericRepository<Classroom>
{
    Task<Classroom> GetClassWithUsersAsync(string id);
}
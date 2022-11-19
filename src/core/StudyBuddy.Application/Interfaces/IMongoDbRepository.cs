using MongoDB.Driver;

namespace StudyBuddy.Application.Interfaces;

public interface IMongoDbRepository<T> where T: class
{
    Task<T> GetAsync(string id);
    Task<IList<T>> GetAllAsync();
    Task InsertOneAsync(T document);
    Task UpdateOneAsync(string id, T document);
    Task UpdateOneAsync(string id, Func<UpdateDefinitionBuilder<T>, UpdateDefinition<T>> update);
    Task DeleteOneAsync(string id);
}
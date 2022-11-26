using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using StudyBuddy.Application.Interfaces;
using StudyBuddy.Domain.Helpers;

namespace StudyBuddy.Persistence.Repositories;

public class MongoDbRepository<T> : IMongoDbRepository<T> where T:class
{ 
    private readonly IMongoCollection<T> _collection;
    
    public MongoDbRepository(IMongoDbSettings settings)
    {
        var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
        _collection = database.GetCollection<T>(GetCollectionName(typeof(T)));
    }
    
    private protected string GetCollectionName(Type documentType)
    {
        return ((BsonCollectionAttribute) documentType.GetCustomAttributes(
                typeof(BsonCollectionAttribute),
                true)
            .FirstOrDefault())?.CollectionName;
    }

    public IMongoCollection<T> Collection()
    {
        return _collection;
    }
    public IQueryable<T> AsQueryable()
    {
        return _collection.AsQueryable();
    }
    public  async Task<T> GetAsync(string id)
    {
        var cursor = await _collection.FindAsync(Builders<T>.Filter.Eq("_id", id));
        return await cursor.FirstOrDefaultAsync();
    }

    public Task<IList<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task InsertOneAsync(T document)
    {
        _collection.InsertOne(document);
    }

    public Task UpdateOneAsync(string id, T document)
    {
        throw new NotImplementedException();
    }

    public Task UpdateOneAsync(string id, Func<UpdateDefinitionBuilder<T>, UpdateDefinition<T>> update)
    {
        throw new NotImplementedException();
    }

    public Task DeleteOneAsync(string id)
    {
        throw new NotImplementedException();
    }
}
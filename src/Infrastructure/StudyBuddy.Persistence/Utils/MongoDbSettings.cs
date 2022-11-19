using StudyBuddy.Application.Interfaces;

namespace StudyBuddy.Persistence.Utils;

public class MongoDbSettings : IMongoDbSettings
{
    public string DatabaseName { get; set; }
    public string ConnectionString { get; set; }
}
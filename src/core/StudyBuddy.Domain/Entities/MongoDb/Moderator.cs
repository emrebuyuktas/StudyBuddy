using StudyBuddy.Domain.Helpers;

namespace StudyBuddy.Domain.Entities.MongoDb;

[BsonCollection("Moderator")]
public class Moderator
{
    public string Id { get; set; }
}
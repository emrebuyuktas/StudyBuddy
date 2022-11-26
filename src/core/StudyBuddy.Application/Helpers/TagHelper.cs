using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Application.Helpers;

public static class TagHelper
{
    public static List<int> TagToList()
    {
        return Enum.GetValues(typeof(Tags)).Cast<Tags>().Select(x=>(int)x).ToList();
    }

    public static List<Tag> TagList(List<Tags> tagsList)
    {
        List <Tag> tags= new();
        foreach (var item in tagsList)
        {
            tags.Add(new Tag
            {
                Id = (int)item,
                Name = item.ToString()
            });
        }

        return tags;
    }
}
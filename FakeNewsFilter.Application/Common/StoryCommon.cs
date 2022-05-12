using System.Threading.Tasks;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;


namespace FakeNewsFilter.Application.Common
{
    public class StoryCommon
    {
        public static async Task<Story> CheckExistStory(ApplicationDBContext _context, int storyId)
        {
            var story = await _context.Story.FindAsync(storyId);
            return story;
        }
    }
}

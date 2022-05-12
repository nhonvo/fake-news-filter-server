using System.Threading.Tasks;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;

namespace FakeNewsFilter.Application.Common
{
    public class TopicCommon
    {
        public static async Task<TopicNews> CheckExistTopic(ApplicationDBContext _context, int topicId)
        {
            var topic = await _context.TopicNews.FindAsync(topicId);
            return topic;
        }
    }
}

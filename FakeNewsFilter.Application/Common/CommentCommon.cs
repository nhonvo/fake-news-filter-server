using System.Threading.Tasks;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;

namespace FakeNewsFilter.Application.Common
{
    public class CommentCommon
    {
        public static async Task<Comment> CheckExistComment(ApplicationDBContext _context, int commentId)
        {
            var comment = await _context.Comment.FindAsync(commentId);
            return comment;
        }
    }
}

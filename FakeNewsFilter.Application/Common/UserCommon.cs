using System;
using System.Threading.Tasks;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;

namespace FakeNewsFilter.Application.Common
{
    public class UserCommon
    {
        public static async Task<User> CheckExistUser(ApplicationDBContext _context, Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user;
        }
    }
}

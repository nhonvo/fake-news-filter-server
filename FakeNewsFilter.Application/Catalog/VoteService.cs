using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.ViewModel.Catalog.Vote;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FakeNewsFilter.Application.Catalog
{
    public interface IVoteService
    {
        Task<ApiResult<bool>> Create(VoteCreateRequest request);

        Task<ApiResult<bool>> Update(VoteUpdateRequest request);
    }

    public class VoteService : IVoteService
    {
        private readonly ApplicationDBContext _context;

        private readonly UserManager<User> _userManager;

        
        public VoteService(ApplicationDBContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<ApiResult<bool>> Create(VoteCreateRequest request)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(request.UserId.ToString());

                if (user == null)
                {
                    return new ApiErrorResult<bool>("UserIsNotExist");
                    
                }

                var news = await _context.News.Include(t => t.NewsInTopics).FirstOrDefaultAsync(t => t.NewsId == request.NewsId);

                if (news == null)
                {
                    return new ApiErrorResult<bool>("NewsIsNotExist");
                }

                var vote = new Data.Entities.Vote()
                {
                    UserId = request.UserId,
                    NewsId = request.NewsId,
                    isReal = request.isReal,
                    Timestamp = DateTime.Now,
                };

                _context.Vote.Add(vote);

                var result = await _context.SaveChangesAsync();

                if (result != 0)
                {
                    return new ApiSuccessResult<bool>("VoteSuccessful", false);
                }

                return new ApiErrorResult<bool>("VoteUnsuccessful");

            }
            catch (Exception ex)
            {
                return new ApiErrorResult<bool>(ex.Message);
            }
        }

        public async Task<ApiResult<bool>> Update(VoteUpdateRequest request)
        {
            var vote_update = await _context.Vote.FirstOrDefaultAsync(u => u.UserId == request.UserId && u.NewsId == request.NewsId);

            if (vote_update == null)
                return new ApiErrorResult<bool>($"CannontFindANewsWithId {request.NewsId}");

            vote_update.isReal = request.isReal;
           
            vote_update.Timestamp = DateTime.Now;

            _context.Vote.Update(vote_update);

            if (await _context.SaveChangesAsync() == 0)
            {
                return new ApiErrorResult<bool>("UpdateVoteUnsuccessful");
            }

            return new ApiSuccessResult<bool>("UpdateVoteSuccessful", false);
        }
    }
}

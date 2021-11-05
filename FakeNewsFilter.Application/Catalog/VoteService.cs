using System;
using System.Linq;
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
                    return new ApiErrorResult<bool>("User is not exist!");
                }

                var news = await _context.News.Include(t => t.NewsInTopics).FirstOrDefaultAsync(t => t.NewsId == request.NewsId);

                if (news == null)
                {
                    return new ApiErrorResult<bool>("News is not exist!");
                }

                var vote = new Data.Entities.Vote()
                {
                    NewsId = request.NewsId,
                    UserId = request.UserId,
                    isReal = request.isReal,
                    Timestamp = DateTime.Now,
                };

                _context.Vote.Add(vote);

                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    return new ApiSuccessResult<bool>("Vote Successful!", false);
                }

                return new ApiErrorResult<bool>("Vote Unsuccessful.");

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
                return new ApiErrorResult<bool>($"Cannont find a news with Id is: {request.NewsId}");

            vote_update.isReal = request.isReal;
           
            vote_update.Timestamp = DateTime.Now;

            _context.Vote.Update(vote_update);

            if (await _context.SaveChangesAsync() == 0)
            {
                return new ApiErrorResult<bool>("Update Vote Unsuccessful! Try again");
            }

            return new ApiSuccessResult<bool>("Update Vote Successful!", false);
        }
    }
}

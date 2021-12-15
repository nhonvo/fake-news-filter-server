using System;
using System.Linq;
using System.Threading.Tasks;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.ViewModel.Catalog.Vote;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FakeNewsFilter.Application.Catalog;

public interface IVoteService
{
    Task<ApiResult<bool>> Create(VoteCreateRequest request);
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

            if (user == null) return new ApiErrorResult<bool>("UserIsNotExist");

            var news = await _context.News.Include(t => t.NewsInTopics)
                .FirstOrDefaultAsync(t => t.NewsId == request.NewsId);

            if (news == null) return new ApiErrorResult<bool>("NewsIsNotExist");

            //Clear existing vote
            var existingVote = _context.Vote.Where(x => x.NewsId == request.NewsId && x.UserId == request.UserId);
            if (existingVote.Any())
            {
                _context.Vote.RemoveRange(existingVote);
                await _context.SaveChangesAsync();
            }

                var vote = new Vote
            {
                UserId = request.UserId,
                NewsId = request.NewsId,
                isReal = request.isReal,
                Timestamp = DateTime.Now
            };

            _context.Vote.Add(vote);

            var result = await _context.SaveChangesAsync();

            if (result != 0) return new ApiSuccessResult<bool>("VoteSuccessful", false);

            return new ApiErrorResult<bool>("VoteUnsuccessful");
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<bool>(ex.Message);
        }
    }

}
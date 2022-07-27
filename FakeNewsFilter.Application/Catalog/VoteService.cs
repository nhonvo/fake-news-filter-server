using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeNewsFilter.Application.Common;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.ViewModel.Catalog.Vote;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLog.Targets;
using Org.BouncyCastle.Asn1.Anssi;

namespace FakeNewsFilter.Application.Catalog;

public interface IVoteService
{
    Task<ApiResult<string>> Create(VoteCreateRequest request);
    Task<ApiResult<bool>> UpdateRatingVote();
}

public class VoteService : IVoteService
{
    private readonly ApplicationDBContext _context;

    public VoteService(ApplicationDBContext context)
    {
        _context = context;
    }

    //Tạo phiếu vote mới
    public async Task<ApiResult<string>> Create(VoteCreateRequest request)
    {
        try
        {
            var user = await UserCommon.CheckExistUser(_context, request.UserId);

            if (user == null) return new ApiErrorResult<string>("UserIsNotExist"," " + request.UserId.ToString());

            var news = await NewsCommon.CheckExistNews(_context, request.NewsId);

            if (news == null) return new ApiErrorResult<string>("NewsIsNotExist"," " + request.NewsId);


            // Xóa phiếu vote hiện có
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

            if (result != 0) return new ApiSuccessResult<string>("VoteSuccessful", " " + result.ToString());

            return new ApiErrorResult<string>("VoteUnsuccessful", " " + result.ToString());
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<string>(ex.Message);
        }
    }

    public async Task<ApiResult<bool>> UpdateRatingVote()
    {
        try
        {
            var newsIds = _context.Vote.Select(x => x.NewsId).Distinct();
            var news = await _context.News.Where(x => newsIds.Contains(x.NewsId)).ToListAsync();
            foreach (var item in news)
            {
                var vote = await _context.Vote.Where(x => x.NewsId == item.NewsId).ToListAsync();
                var count = (double) vote.Count;
                var real = (double) vote.Count(x => x.isReal);
                var rating = real / count;

                item.SocialBeliefs = Math.Round(rating, 2);
                _context.News.Update(item);
            }
            
            var result = await _context.SaveChangesAsync();
            if (result != 0) return new ApiSuccessResult<bool>("Update Rating Vote Successful" +  result);
            return new ApiErrorResult<bool>("Update Rating Vote Unsuccessful" + result);
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<bool>(ex.Message);
        }

    }

}
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.ViewModel.Catalog.Follows;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FakeNewsFilter.Application.Catalog
{
    public interface IFollowService
    {
        Task<ApiResult<bool>> Create(FollowCreateRequest request);
        Task<ApiResult<bool>> Update(FollowUpdateRequest request);
    }
    public class FollowService : IFollowService
    {
        private readonly ApplicationDBContext _context;

        private readonly UserManager<User> _userManager;

        public FollowService(ApplicationDBContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ApiResult<bool>> Create(FollowCreateRequest request)
        {
            try
            {
                var User = await _userManager.FindByIdAsync(request.UserId.ToString());
                    if (User == null)
                    {
                            return new ApiErrorResult<bool>("UserIsNotExist");
                    }

                foreach (var item in request.TopicId)
                {
                    var Topic = await _context.TopicNews.FirstOrDefaultAsync(t => t.TopicId == item);
                    if (Topic == null)
                    {
                        return new ApiErrorResult<bool>("CannontFindATopicWithId");
                    }
                }

                foreach (var item in request.TopicId)
                {
                    var follow = new Data.Entities.Follow()
                    {
                        UserId = request.UserId,
                        TopicId = item
                    };
                    _context.Follow.Add(follow);
                }
                
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    return new ApiSuccessResult<bool>("FollowSuccessful", false);
                }

                return new ApiErrorResult<bool>("FollowUnsuccessful");

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ApiResult<bool>> Update(FollowUpdateRequest request)
        {
            //check user
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("UserIsNotExist");
            }

            //check topic id
            foreach (var item in request.TopicId)
            {
                var topic = await _context.TopicNews.FirstOrDefaultAsync(t => t.TopicId == item);
                if (topic == null)
                {
                    return new ApiErrorResult<bool>("CannontFindATopicWithId");
                }
            }
            // remove user
            var follow = _context.Follow.Where(t => t.UserId == request.UserId);
            _context.Follow.RemoveRange(follow);
            await _context.SaveChangesAsync();
            // update follow topic
            foreach (var item in request.TopicId)
            {
                var followUpdate = new Data.Entities.Follow()
                {
                    UserId = request.UserId,
                    TopicId = item
                };
                _context.Follow.Add(followUpdate);
            }
            
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return new ApiSuccessResult<bool>("FollowUpdateSuccessful", false);
            }

            return new ApiErrorResult<bool>("FollowUpdateUnsuccessful");
        }

    }
}

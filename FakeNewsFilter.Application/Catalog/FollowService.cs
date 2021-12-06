using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.ViewModel.Catalog.Follows;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeNewsFilter.Application.Catalog
{
    public interface IFollowService
    {
        Task<ApiResult<List<int>>> GetFollowTopicByUser (Guid userId);
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
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                    if (user == null)
                    {
                            return new ApiErrorResult<bool>("UserIsNotExist");
                    }

                foreach (var item in request.TopicId)
                {
                    var topic = await _context.TopicNews.FirstOrDefaultAsync(t => t.TopicId == item);
                    if (topic == null)
                    {
                        return new ApiErrorResult<bool>("CannontFindATopicWithId");
                    }
                }

                foreach (var item in request.TopicId)
                {
                    var follow = _context.Follow.Where(t => t.TopicId == item);
                    _context.Follow.RemoveRange(follow);
                    await _context.SaveChangesAsync();
                }

                foreach (var item in request.TopicId)
                {
                    var followCreate = new Data.Entities.Follow()
                    {
                        UserId = request.UserId,
                        TopicId = item
                    };
                    _context.Follow.Add(followCreate);
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
        //Get Follow Topic By User
        public async Task<ApiResult<List<int>>> GetFollowTopicByUser(Guid userId)
        {
            //Check user exist
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return new ApiErrorResult<List<int>>("AccountDoesNotExist");
            }

            //get topicid form table follow
            var listFollowTopic =  _context.Follow.Where(x => x.UserId == userId).Select(x => x.TopicId).ToList();

            if (listFollowTopic == null)
            {
                return new ApiErrorResult<List<int>>("GetListFollowUnsuccessful");
            }
            return new ApiSuccessResult<List<int>>("GetListFollowSuccessful", listFollowTopic);
        }

    }
}

using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.ViewModel.Catalog.Follows;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FakeNewsFilter.Application.Catalog
{
    public interface IFollowService
    {
        Task<ApiResult<List<int>>> GetFollowTopicByUser (Guid userId);
        Task<ApiResult<string>> Create(FollowCreateRequest request);
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

        //Tạo follow mới
        public async Task<ApiResult<string>> Create(FollowCreateRequest request)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                    if (user == null)
                    {
                        return new ApiErrorResult<string>("UserIsNotExist", " " + request.UserId.ToString());
                    }
                    
                    var follow = _context.Follow.Where(t => t.UserId == request.UserId);
                    if (follow.Any())
                    {
                        _context.Follow.RemoveRange(follow);
                        await _context.SaveChangesAsync();
                    }
                    
                    foreach (var item in request.TopicId)
                    {
                        var topic = await _context.TopicNews.FirstOrDefaultAsync(t => t.TopicId == item);
                        if (topic == null)
                        {
                            return new ApiErrorResult<string>("CannontFindATopicWithId", " " + request.TopicId.ToString());
                        }
                        
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
                        transaction.Commit();
                        return new ApiSuccessResult<string>("FollowSuccessful", " " + result.ToString());
                    }
                    transaction.Rollback();
                    return new ApiErrorResult<string>("FollowUnsuccessful"," " + result.ToString());

                }
                catch (DbUpdateException ex)
                {
                    transaction.Rollback();
                    return new ApiErrorResult<string>(ex.Message);
                }
            }
                
        }
        
        //Lấy follow chủ đề theo người dùng
        public async Task<ApiResult<List<int>>> GetFollowTopicByUser(Guid userId)
        {
            //Kiểm tra người dùng có tồn tại hay không
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return new ApiErrorResult<List<int>>("AccountDoesNotExist");
            }

            //Lấy topicid từ bảng follow
            var listFollowTopic =  _context.Follow.Where(x => x.UserId == userId).Select(x => x.TopicId).ToList();

            if (listFollowTopic == null)
            {
                return new ApiErrorResult<List<int>>("GetListFollowUnsuccessful");
            }
            return new ApiSuccessResult<List<int>>("GetListFollowSuccessful", listFollowTopic);
        }

    }
}

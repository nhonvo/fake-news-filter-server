using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.ViewModel.Catalog.Comment;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeNewsFilter.Application.Catalog
{
    public interface ICommentService
    {
        Task<ApiResult<bool>> Create(CommentCreateRequest request);
        Task<ApiResult<bool>> Delete(int commentId, Guid userId);
        Task<ApiResult<List<CommentViewModel>>> GetCommentByNewsId(int NewsId);
        Task<ApiResult<bool>> Update(CommentUpdateRequest request);
    }
    public class CommentService : ICommentService
    {
        private readonly ApplicationDBContext _context;

        private readonly UserManager<User> _userManager;

        public CommentService(ApplicationDBContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ApiResult<bool>> Create(CommentCreateRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("UserIsNotExist");
            }

            var news = await _context.Comment.FirstOrDefaultAsync(t => t.NewsId == request.NewsId);
            if (news == null)
            {
                return new ApiErrorResult<bool>("CommentIsNotExist");
            }

            var comments = new Comment()
            {
                NewsId = request.NewsId,
                UserId = request.UserId,
                Content = request.Content,
                Timestamp = DateTime.Now
            };

            _context.Comment.Add(comments);

            var result = await _context.SaveChangesAsync();

            if (result != 0)
            {
                return new ApiSuccessResult<bool>("CommentSuccessful", false);
            }

            return new ApiErrorResult<bool>("CommentUnsuccessful");
        }

        public async Task<ApiResult<List<CommentViewModel>>> GetCommentByNewsId(int newsId)
        {
            var query = from n in _context.Comment
                        select new { n };

            query = query.Where(t => newsId == t.n.NewsId);

            var data = await query
                .Select(x => new CommentViewModel()
                {
                    NewsId = x.n.NewsId,
                    CommentId = x.n.CommentId,
                    Timestamp = x.n.Timestamp,
                    UserId = x.n.UserId,
                    Content = x.n.Content,
                }).ToListAsync();

            if (data == null)
            {
                return new ApiErrorResult<List<CommentViewModel>>("GetCommentByNewsIdUnsuccessful");
            }
            if (data.Count == 0)
            {
                return new ApiSuccessResult<List<CommentViewModel>>("DoNotHaveGetCommentByNewsId");
            }
            return new ApiSuccessResult<List<CommentViewModel>>("GetCommentByNewsIdSuccessful", data);
        }

        public async Task<ApiResult<bool>> Delete(int commentId, Guid userId)
        {
            var comment = await _context.Comment.FirstOrDefaultAsync(x => x.CommentId == commentId);
            if (comment == null)
                return new ApiErrorResult<bool>("CommentNotFound");

            var userComment = _userManager.Users.FirstOrDefault(x => x.Id == comment.UserId);
            var userLogin = _userManager.Users.FirstOrDefault(x => x.Id == userId);

            if (userComment != userLogin)
                return new ApiErrorResult<bool>("YouCanOnlyDeleteYourComment");

            _context.Comment.Remove(comment);

            if (await _context.SaveChangesAsync() == 0)
            {
                return new ApiErrorResult<bool>("DeleteCommentUnsuccessful");
            }

            return new ApiSuccessResult<bool>("DeleteCommentSuccessful", false);
        }

        public async Task<ApiResult<bool>> Update(CommentUpdateRequest request)
        {
            var comment = await _context.Comment.FirstOrDefaultAsync(x => x.CommentId == request.CommentId);
            if(comment == null)
            {
                return new ApiErrorResult<bool>("ComentNotFound");
            }

            comment.Content = request.Content;
            comment.Timestamp = DateTime.Now;

            _context.Comment.Update(comment);
            if (await _context.SaveChangesAsync() == 0)
            {
                return new ApiErrorResult<bool>("UpdateCommentUnsuccessful");
            }

            return new ApiSuccessResult<bool>("UpdateCommentSuccessful", false);
        }
    }
}

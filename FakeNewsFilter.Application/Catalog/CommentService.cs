using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.ViewModel.Catalog.Comment;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FakeNewsFilter.Application.Catalog;

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
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public CommentService(ApplicationDBContext context, UserManager<User> userManager, IMapper mapper)
    {
        _context = context;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<ApiResult<bool>> Create(CommentCreateRequest request)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null) return new ApiErrorResult<bool>("UserIsNotExist");

        var news = await _context.News.Include(x => x.NewsInTopics)
            .FirstOrDefaultAsync(t => t.NewsId == request.NewsId);
        if (news == null) return new ApiErrorResult<bool>("NewsIsNotExist");

        var comments = new Comment
        {
            NewsId = request.NewsId,
            UserId = request.UserId,
            Content = request.Content,
            ParentId = request.ParentId,
            Timestamp = DateTime.Now
        };

        _context.Comment.Add(comments);

        var result = await _context.SaveChangesAsync();

        if (result != 0) return new ApiSuccessResult<bool>("CreateCommentSuccessful", false);

        return new ApiErrorResult<bool>("CreateCommentUnsuccessful");
    }

    public async Task<ApiResult<List<CommentViewModel>>> GetCommentByNewsId(int newsId)
    {
        //check whether news is exist
        var news = await _context.News.FirstOrDefaultAsync(x => x.NewsId == newsId);
        if (news == null) return new ApiErrorResult<List<CommentViewModel>>("NewsNotFound");

        var commentsList = await _context.Comment.Where(x => x.NewsId == newsId).ToListAsync();
        var map = new Dictionary<int, CommentViewModel>();
        var result = new List<CommentViewModel>();

        foreach (var comment in commentsList)
        {
            var commentViewModel = _mapper.Map<CommentViewModel>(comment);

            map.Add(comment.CommentId, commentViewModel);
            
            if (comment.ParentId == null)
            {
                result.Add(commentViewModel);
            }
            else
            {
                var parentComment = map[(int) comment.ParentId];

                if (parentComment.Child == null) parentComment.Child = new List<CommentViewModel>();

                parentComment.Child.Add(commentViewModel);
            }
        }
        
        if (result.Count == 0) return new ApiSuccessResult<List<CommentViewModel>>("DoNotHaveGetCommentByNewsId");
        return new ApiSuccessResult<List<CommentViewModel>>("GetCommentByNewsIdSuccessful", result);
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

        if (await _context.SaveChangesAsync() == 0) return new ApiErrorResult<bool>("DeleteCommentUnsuccessful");

        return new ApiSuccessResult<bool>("DeleteCommentSuccessful", false);
    }

    public async Task<ApiResult<bool>> Update(CommentUpdateRequest request)
    {
        var comment = await _context.Comment.FirstOrDefaultAsync(x => x.CommentId == request.CommentId);
        if (comment == null) return new ApiErrorResult<bool>("CommentNotFound");

        comment.Content = request.Content;
        comment.Timestamp = DateTime.Now;

        _context.Comment.Update(comment);
        if (await _context.SaveChangesAsync() == 0) return new ApiErrorResult<bool>("UpdateCommentUnsuccessful");

        return new ApiSuccessResult<bool>("UpdateCommentSuccessful", false);
    }
}
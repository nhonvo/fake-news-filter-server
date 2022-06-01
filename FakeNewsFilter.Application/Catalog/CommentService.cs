using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FakeNewsFilter.Application.Common;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.ViewModel.Catalog.Comment;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FakeNewsFilter.Application.Catalog;

public interface ICommentService
{
    Task<ApiResult<string>> Create(CommentCreateRequest request);
    Task<ApiResult<string>> Delete(int commentId, Guid userId);
    Task<ApiResult<List<CommentViewModel>>> GetCommentByNewsId(int NewsId);
    Task<ApiResult<string>> Update(CommentUpdateRequest request);
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

    //Tạo bình luận
    public async Task<ApiResult<string>> Create(CommentCreateRequest request)
    {
        var user = await UserCommon.CheckExistUser(_context, request.UserId);
        if (user == null) return new ApiErrorResult<string>("UserIsNotExist"," " + request.UserId.ToString());

        var news = await NewsCommon.CheckExistNews(_context, request.NewsId);
        if (news == null) return new ApiErrorResult<string>("NewsIsNotExist", " " + request.NewsId);

        Comment comment = null;

        if(request.ParentId != 0)
        {
            var checkParentId = await _context.Comment.FirstOrDefaultAsync(x => x.CommentId == request.ParentId);
            if(checkParentId != null)
            {
                comment = new Comment
                {
                    NewsId = request.NewsId,
                    UserId = request.UserId,
                    Content = request.Content,
                    ParentId = request.ParentId,
                    Timestamp = DateTime.Now
                };
            }

        }
        else
        {
            comment = new Comment
            {
                NewsId = request.NewsId,
                UserId = request.UserId,
                Content = request.Content,
                Timestamp = DateTime.Now
            };
        }

        _context.Comment.Add(comment);

        var result = await _context.SaveChangesAsync();

        if (result != 0) return new ApiSuccessResult<string>("CreateCommentSuccessful", " " + comment.CommentId.ToString());

        return new ApiErrorResult<string>("CreateCommentUnsuccessful"," " + result);
    }

    //Lấy tất cả các bình luận của tin tức đó
    public async Task<ApiResult<List<CommentViewModel>>> GetCommentByNewsId(int newsId)
    {
        //Kiểm tra tin tức có tồn tại hay không
        var news = await NewsCommon.CheckExistNews(_context, newsId);
        if (news == null) return new ApiErrorResult<List<CommentViewModel>>("NewsIsNotExist", newsId);

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

    //xóa bình luận
    public async Task<ApiResult<string>> Delete(int commentId, Guid userId)
    {
        var comment = await CommentCommon.CheckExistComment(_context, commentId);
        if (comment == null)
            return new ApiErrorResult<string>("CommentNotFound"," " + commentId.ToString());

        var userComment = await UserCommon.CheckExistUser(_context, comment.UserId);
        var userLogin = await UserCommon.CheckExistUser(_context, userId);

        if (userComment != userLogin)
            return new ApiErrorResult<string>("YouCanOnlyDeleteYourComment", " " + userId.ToString());

        _context.Comment.Remove(comment);
        var result = await _context.SaveChangesAsync();
        if (result == 0) return new ApiErrorResult<string>("DeleteCommentUnsuccessful", " " + result);

        return new ApiSuccessResult<string>("DeleteCommentSuccessful", " " + comment.CommentId.ToString());
    }

    //Cập nhật bình luận
    public async Task<ApiResult<string>> Update(CommentUpdateRequest request)
    {
        var comment = await CommentCommon.CheckExistComment(_context, request.CommentId);
        if (comment == null) return new ApiErrorResult<string>("CommentNotFound", " " + request.CommentId);

        comment.Content = request.Content;
        comment.Timestamp = DateTime.Now;

        _context.Comment.Update(comment);
        var result = await _context.SaveChangesAsync();
        if (result == 0) return new ApiErrorResult<string>("UpdateCommentUnsuccessful", " " + result);

        return new ApiSuccessResult<string>("UpdateCommentSuccessful", " " + comment.CommentId.ToString());
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FakeNewsFilter.Application.Common;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.Data.Enums;
using FakeNewsFilter.ViewModel.Catalog.Comment;
using FakeNewsFilter.ViewModel.Common;
using FakeNewsFilter.ViewModel.System.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FakeNewsFilter.Application.Catalog;

public interface ICommentService
{
    Task<ApiResult<CommentViewModel>> Create(CommentCreateRequest request);
    Task<ApiResult<string>> Delete(int commentId, Guid userId);
    Task<ApiResult<List<CommentViewModel>>> GetCommentByNewsId(int NewsId);
    Task<ApiResult<CommentViewModel>> GetCommentById(int commentId);
    Task<ApiResult<List<CommentViewModel>>> Update(CommentUpdateRequest request);
    Task<ApiResult<List<CommentViewModel>>> Archive(CommentUpdateRequest request);
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
    public async Task<ApiResult<CommentViewModel>> Create(CommentCreateRequest request)
    {
        var user = await UserCommon.CheckExistUser(_context, request.UserId);
        if (user == null)
            return new ApiErrorResult<CommentViewModel>(404, "UserIsNotExist");

        var news = await NewsCommon.CheckExistNews(_context, request.NewsId);
        if (news == null)
            return new ApiErrorResult<CommentViewModel>(404, "NewsIsNotExist");

        Comment comment = null;
        
        if (request.ParentId != 0)
        {
            var checkParentId = await _context.Comment.FirstOrDefaultAsync(x => x.CommentId == request.ParentId);
            if (checkParentId != null)
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
        var cmtModel = await GetCommentById(comment.CommentId);
        if (result != 0)
            return new ApiSuccessResult<CommentViewModel>("CreateCommentSuccessful");
        return new ApiErrorResult<CommentViewModel>(400, "CreateCommentUnsuccessful");
    }

    public async Task<ApiResult<CommentViewModel>> GetCommentById(int commentId)
    {
        var cmt = await _context.Comment.FirstOrDefaultAsync(x => x.CommentId == commentId);

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == cmt.UserId);
        
        if (user == null)
            return new ApiErrorResult<CommentViewModel>(404, "UserIsNotExist");
        
        var userViewModel = new UserViewModel()
        {
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Avatar = user.Avatar?.PathMedia
        };

        if (cmt == null)
            return new ApiErrorResult<CommentViewModel>(404, "CommentNotFound");
        
        var cmtViewModel = new CommentViewModel()
        {
            CommentId = cmt.CommentId,
            NewsId = cmt.NewsId,
            UserId = cmt.UserId,
            User = userViewModel,
            Content = cmt.Content,
            Timestamp = cmt.Timestamp
        };

        if (cmt.ParentId != null)
        {
            cmtViewModel.ParentId = (int) cmt.ParentId;
        }

        return new ApiSuccessResult<CommentViewModel>("Get comment successfully", cmtViewModel);
    }

    //Lấy tất cả các bình luận của tin tức đó
    public async Task<ApiResult<List<CommentViewModel>>> GetCommentByNewsId(int newsId)
    {
        //Kiểm tra tin tức có tồn tại hay không
        var news = await NewsCommon.CheckExistNews(_context, newsId);

        if (news == null)
            return new ApiErrorResult<List<CommentViewModel>>(404, "NewsIsNotExist");

        var commentsList = await _context.Comment.Where(x => x.NewsId == newsId).ToListAsync();

        var map = new Dictionary<int, CommentViewModel>();

        var result = new List<CommentViewModel>();

        foreach (var comment in commentsList)
        {
            var commentViewModel = _mapper.Map<CommentViewModel>(comment);

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == comment.UserId);

            var userViewModel = new UserViewModel()
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Avatar = user.Avatar?.PathMedia,
            };

            commentViewModel.User = userViewModel;

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
            return new ApiErrorResult<string>(404, "CommentNotFound", " " + commentId.ToString());

        var userComment = await UserCommon.CheckExistUser(_context, comment.UserId);
        var userLogin = await UserCommon.CheckExistUser(_context, userId);

        if (userComment != userLogin)
            return new ApiErrorResult<string>(400, "YouCanOnlyDeleteYourComment", " " + userId.ToString());

        _context.Comment.Remove(comment);
        var result = await _context.SaveChangesAsync();
        if (result == 0)
            return new ApiErrorResult<string>(400, "DeleteCommentUnsuccessful", " " + result);

        return new ApiSuccessResult<string>("DeleteCommentSuccessful", " " + comment.CommentId.ToString());
    }

    //Cập nhật bình luận
    public async Task<ApiResult<List<CommentViewModel>>> Update(CommentUpdateRequest request)
    {
        var comment = await CommentCommon.CheckExistComment(_context, request.CommentId);
        if (comment == null)
            return new ApiErrorResult<List<CommentViewModel>>(404, "CommentNotFound");

        comment.Content = request.Content;
        comment.Timestamp = DateTime.Now;

        _context.Comment.Update(comment);
        var result = await _context.SaveChangesAsync();
        var cmtModel = await GetCommentByNewsId(comment.CommentId);
        if (result == 0)
            return new ApiErrorResult<List<CommentViewModel>>(400, "UpdateCommentUnsuccessful", cmtModel.ResultObj);

        return new ApiSuccessResult<List<CommentViewModel>>("UpdateCommentSuccessful", cmtModel.ResultObj);
    }

    public async Task<ApiResult<List<CommentViewModel>>> Archive(CommentUpdateRequest request)
    {
        var cmt_update = await CommentCommon.CheckExistComment(_context, request.CommentId);

        if (cmt_update == null)
            return new ApiErrorResult<List<CommentViewModel>>(404, "CannontFindCommentWithId");

        cmt_update.Status = Status.Archive;
        var cmtModel = await GetCommentByNewsId(cmt_update.CommentId);
        var result = await _context.SaveChangesAsync();

        if (result == 0)
            return new ApiErrorResult<List<CommentViewModel>>(400, "UpdateLinkNewsUnsuccessful", cmtModel.ResultObj);

        return new ApiSuccessResult<List<CommentViewModel>>("UpdateLinkNewsSuccessful", cmtModel.ResultObj);
    }
}
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.ViewModel.Catalog.Comment;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeNewsFilter.Application.Catalog
{
    public interface ICommentService
    {
        //Task<ApiResult<bool>> Create(CommentCreateRequest request);
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

        //public async Task<ApiResult<bool>> Create(CommentCreateRequest request)
        //{
        //    return;
        //}

    }
}

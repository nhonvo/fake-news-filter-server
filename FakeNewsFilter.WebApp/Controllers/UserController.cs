using System;
using System.Threading.Tasks;
using FakeNewsFilter.AdminApp.Controllers;
using FakeNewsFilter.ViewModel.System.Users;
using FakeNewsFilter.WebApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FakeNewsFilter.WebApp.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserApiClient _userApiClient;

        public UserController(IUserApiClient userApiClient, IConfiguration configuration)
        {
            _userApiClient = userApiClient;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index(string keyWord,int pageIndex = 1, int pageSize = 10)
        {

            var request = new GetUserPagingRequest()
            {
                keyWord = keyWord,
                pageIndex = pageIndex,
                pageSize = pageSize
            };

            var data = await _userApiClient.GetUsersPaging(request);

            ViewBag.Keyword = keyWord;

            if(TempData["result"]!=null)
            {
                ViewBag.SuccessMsg = TempData["Result"];
            }

            return View(data.ResultObj);
        }

       
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterRequest request)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return View();
            }

            var result = await _userApiClient.RegisterUser(request);

            if(result.IsSuccessed)
            {
                TempData["Result"] = $"Create User {request.UserName} successful!";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);

            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _userApiClient.GetById(id);

            if (result.IsSuccessed)
            {
                var user = result.ResultObj;

                var updateRequest = new UserUpdateRequest()
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    Name = user.FullName,
                    UserName = user.UserName,
                    PhoneNumber = user.PhoneNumber
                };

                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.UpdateUser(request.UserId, request);
            if (result.IsSuccessed)
            {
                TempData["result"] = $"Update User {request.UserName} successful!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(String UserId)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.Delete(UserId);
            if (result.IsSuccessed)
            {
                TempData["result"] = $"Delete User {UserId} successful!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return RedirectToAction("Index");
        }

    }
}

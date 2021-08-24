using System;
using System.Linq;
using System.Threading.Tasks;
using FakeNewsFilter.AdminApp.Controllers;
using FakeNewsFilter.AdminApp.Services;
using FakeNewsFilter.ViewModel.Common;
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

    public class UserController : BaseController
    {
        private readonly IUserApiClient _userApiClient;

        private readonly IRoleApiClient _roleApiClient;

        public UserController(IUserApiClient userApiClient, IRoleApiClient roleApiClient)
        {
            _userApiClient = userApiClient;
            _roleApiClient = roleApiClient;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var data = await _userApiClient.GetUsers();

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["Result"];
            }

            return Json(new
            {
                data = data.ResultObj
            });

        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterRequest request)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return RedirectToAction("Index");
            }

            var result = await _userApiClient.RegisterUser(request);

            if(result.IsSuccessed)
            {
                TempData["Result"] = $"Create User {request.UserName} successful!";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);

            return RedirectToAction("Index", request);
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

        [HttpGet]
        public async Task<IActionResult> RoleAssign(Guid id)
        {
            var roleAssignRequest = await GetRoleAssignRequest(id);
            return View(roleAssignRequest);
        }


        //Cập nhật quyền cho người dùng
        [HttpPost]
        public async Task<IActionResult> RoleAssign(RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.RoleAssign(request.Id, request);

            if (result.IsSuccessed)
            {
                TempData["result"] = "Update Role Successful!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);

            var roleAssignRequest = await GetRoleAssignRequest(request.Id);

            return View(roleAssignRequest);
        }

        private async Task<RoleAssignRequest> GetRoleAssignRequest(Guid id)
        {
            var userObj = await _userApiClient.GetById(id);
            var roleObj = await _roleApiClient.GetAll();
            var roleAssignRequest = new RoleAssignRequest();
            foreach (var role in roleObj.ResultObj)
            {
                roleAssignRequest.Roles.Add(new SelectItem()
                {
                    Id = role.Id.ToString(),
                    Name = role.Name,
                    Selected = userObj.ResultObj.Roles.Contains(role.Name)
                });
            }
            return roleAssignRequest;
        }


    }
}

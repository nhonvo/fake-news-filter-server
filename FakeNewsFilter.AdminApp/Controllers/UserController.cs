﻿using System;
using System.Threading.Tasks;
using FakeNewsFilter.AdminApp.Controllers;
using FakeNewsFilter.ClientServices;
using FakeNewsFilter.ViewModel.Common;
using FakeNewsFilter.ViewModel.System.Roles;
using FakeNewsFilter.ViewModel.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FakeNewsFilter.WebApp.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserApi _userApi;

        private readonly IRoleApi _roleApi;

        public UserController(IUserApi userApi, IRoleApi roleApi)
        {
            _userApi = userApi;
            _roleApi = roleApi;
        }

        [Breadcrumb("User Manager")]
        public async Task<IActionResult> Index()
        {
            var data = await _userApi.GetUsers();

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["Result"];
            }
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }
            return View(data);
        }


        [HttpPost]
        public async Task<IActionResult> Create(RegisterRequest request)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return RedirectToAction("Index");
            }

            var result = await _userApi.RegisterUser(request);

            if(result.StatusCode == 200)
            {
                TempData["OigetitNews"] = $"Create User {request.UserName} successful!";

                return RedirectToAction("Index");
            }

            TempData["Error"] = result.Message;
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Breadcrumb("Edit User", FromController = typeof(UserController), FromPage = typeof(Index))]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = new UserRoleViewModel();

            var result = await _userApi.GetById(id);

            if (result.StatusCode == 200)
            {
                var user = result.ResultObj;

                var updateRequest = new UserUpdateRequest()
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    Name = user.FullName,
                    UserName = user.UserName,
                    Avatar = user.Avatar ?? "default.png"
                };

                var roleAssignRequest = await GetRoleAssignRequest(id);

                model.EditUser = updateRequest;

                model.EditRole = roleAssignRequest;

                return View(model);
            }

            return View("Index");
            
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApi.UpdateUser(request);
            if (result.StatusCode == 200)
            {
                TempData["result"] = $"Update User {request.UserName} successful!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(String UserId)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return RedirectToAction("Index");
            }

            var result = await _userApi.Delete(UserId);
            if (result.StatusCode == 200)
            {
                TempData["result"] = $"Delete User successful!";
                return Json("done");
            }

            ModelState.AddModelError("", result.Message);
            return Json("error");
        }

        [HttpGet]
        public async Task<IActionResult> RoleAssign(Guid id)
        {
            var roleAssignRequest = await GetRoleAssignRequest(id);
            return View("EditRole", roleAssignRequest);
        }


        //Cập nhật quyền cho người dùng
        [HttpPost]
        public async Task<IActionResult> RoleAssign(RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return RedirectToAction("Index");
            }

            var result = await _userApi.RoleAssign(request.Id, request);

            if (result.StatusCode == 200)
            {
                TempData["result"] = "Update Role Successful!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);

            var roleAssignRequest = await GetRoleAssignRequest(request.Id);

            return View("EditRole", roleAssignRequest);
        }

        private async Task<RoleAssignRequest> GetRoleAssignRequest(Guid id)
        {
            var userObj = await _userApi.GetById(id);
            var roleObj = await _roleApi.GetAll();
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

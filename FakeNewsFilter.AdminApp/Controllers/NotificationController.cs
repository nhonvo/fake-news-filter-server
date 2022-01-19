using System.Threading.Tasks;
using FakeNewsFilter.AdminApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FakeNewsFilter.AdminApp.Controllers
{
    public class NotificationController : BaseController
    {
        private readonly NotificationApi _notificationApi;

        public NotificationController(NotificationApi notificationApi)
        {
            _notificationApi = notificationApi;
        }

        // GET
        public async Task<IActionResult> Index()
        {
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["Result"];
            }

            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetViewNotifications()
        {
            var data = await _notificationApi.GetNotifications();

            return Json(new
            {
                data = data.notifications,
            });
        }
    }
}
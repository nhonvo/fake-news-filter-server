using System.Threading.Tasks;
using FakeNewsFilter.ClientService;
using FakeNewsFilter.ViewModel.Catalog.Notification;
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
        public IActionResult Index()
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

        [HttpGet]
        public async Task<IActionResult> GetNotification(string id)
        {
            var data = await _notificationApi.GetNotification(id);

            return PartialView("GetNotification", data);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateNotificationRequest createNotificationRequest)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return RedirectToAction("Index");
            }
            
            
            var result = await _notificationApi.CreateNotification(createNotificationRequest);
            
            if (result.StatusCode == 200)
            {
                TempData["Result"] = $"Create Notification Successful!";
            
                return RedirectToAction("Index");
            }
            
            TempData["Error"] = result.Message;

            return RedirectToAction("Index");
        }
        
    }
}
using Microsoft.AspNetCore.Mvc;

namespace EPYTST.WEB.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            if (TempData["User"] != null)
            {
                var userJson = TempData["User"].ToString();
                ViewBag.User = userJson;
            }

            return View();
        }
    }
}

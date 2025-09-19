using Microsoft.AspNetCore.Mvc;

namespace API_MVC_Suptech.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

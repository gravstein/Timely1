using Microsoft.AspNetCore.Mvc;

namespace Timely1.Controllers
{
    public class GuitarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace ORMFinal.Controllers
{
    public class AnimalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

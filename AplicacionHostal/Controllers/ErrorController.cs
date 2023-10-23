using Microsoft.AspNetCore.Mvc;

namespace AplicacionHostal.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult NoAutorizado()
        {
            return View();
        }
    }
}

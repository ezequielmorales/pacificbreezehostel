using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AplicacionHostal.Controllers
{
    public class RegistroController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UsuarioLogueado") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("NoAutorizado", "Error");
            }
        }

        [HttpPost]
        public IActionResult Registrar(Huesped hue)
        {                       
            try
            {                
                Sistema.ObtenerInstancia.AltaHuesped(hue);                
                HttpContext.Session.SetString("UsuarioLogueado", hue.Correo!);
                HttpContext.Session.SetString("UsuarioRol", hue.Rol!);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["RegistroError"] = ex.Message;                
                return RedirectToAction("Index", "Registro");
            }            
        }
    }
}

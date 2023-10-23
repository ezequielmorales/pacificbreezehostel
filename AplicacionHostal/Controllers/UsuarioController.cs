using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace AplicacionHostal.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult VerDatos()
        {
            if (HttpContext.Session.GetString("UsuarioRol") == "OPERADOR" || HttpContext.Session.GetString("UsuarioRol") == "HUESPED")
            {
                Usuario? usuario = Sistema.ObtenerInstancia.ObtenerUsuarioPorEmail(HttpContext.Session.GetString("UsuarioLogueado")!);
                return View(usuario);
            }
            else
            {
                return RedirectToAction("NoAutorizado", "Error");
            }
        }
    }
}

using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AplicacionHostal.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {  
            if(HttpContext.Session.GetString("UsuarioRol") == null){
                return View();
            }
            else
            {
                return RedirectToAction("NoAutorizado", "Error");
            }
        }

        [HttpPost]
        public IActionResult Loguear(Usuario usu)
        {
            try
            {                
                Usuario usuario = Sistema.ObtenerInstancia.LoguearUsuario(usu);
                HttpContext.Session.SetString("UsuarioLogueado", usuario.Correo!);
                HttpContext.Session.SetString("UsuarioRol", usuario.Rol!);                

            }catch (Exception ex)
            {
                TempData["LoginError"] = ex.Message;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Desloguear() 
        {
            HttpContext.Session.Remove("UsuarioLogueado");
            HttpContext.Session.Remove("UsuarioRol");
            return RedirectToAction("Index", "Home");
        }
    }
}

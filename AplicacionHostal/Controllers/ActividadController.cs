using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace AplicacionHostal.Controllers
{
    public class ActividadController : Controller
    {
        public IActionResult Listar(DateTime fecha)
        {
            List<Actividad> listado = new List<Actividad>();
            try
            {
                listado = Sistema.ObtenerInstancia.ListarActividades(fecha);                
            }
            catch (Exception ex)
            {
                ViewBag.MensajeError = ex.Message;
            }
            
            return View(listado);
        }       
    }
}

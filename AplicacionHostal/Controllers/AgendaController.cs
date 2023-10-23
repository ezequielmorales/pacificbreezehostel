using Dominio;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AplicacionHostal.Controllers
{
    public class AgendaController : Controller
    {
        public IActionResult Agendar(int elID)
        {
            if (HttpContext.Session.GetString("UsuarioRol") == "HUESPED")
            {
                Agenda agenda = new Agenda();
                try
                {                
                    Huesped? huesped = Sistema.ObtenerInstancia.ObtenerUsuarioPorEmail(HttpContext.Session.GetString("UsuarioLogueado")!) as Huesped;                
                    Actividad actividad = Sistema.ObtenerInstancia.ObtenerActividadPorId(elID);
                    agenda = Sistema.ObtenerInstancia.AgendarActividad(actividad, huesped!);
                }
                catch (Exception ex)
                {
                    ViewBag.MensajeError = ex.Message;
                }
                return View(agenda);
            }
            else
            {
                return RedirectToAction("NoAutorizado", "Error");
            }
        }

        public IActionResult MostrarAgenda() //Comparte la View() con el Action MostrarAgendasPendientes
        {
            if (HttpContext.Session.GetString("UsuarioRol") == "HUESPED")
            {
                List<Agenda> listado = new List<Agenda>();
                try
                {
                    listado = Sistema.ObtenerInstancia.ObtenerAgendaPorEmail(HttpContext.Session.GetString("UsuarioLogueado")!);
                    TempData["MostrarBotonConfirmar"] = false;
                }
                catch (Exception ex)
                {
                    ViewBag.MensajeError = ex.Message;
                }
                return View(listado);
            }
            else
            {
                return RedirectToAction("NoAutorizado", "Error");
            }
        } 

        public IActionResult MostrarAgendaSegunDocumento(TipoDocumento tipoDocumento, string numeroDocumento) //Comparte la View() con el Action MostrarAgendaSegunFecha()
        {
            if (HttpContext.Session.GetString("UsuarioRol") == "OPERADOR")
            {
                List<Agenda> listado = new List<Agenda>();
                try
                {
                    TempData["tipo"] = "DOCUMENTO"; //Reutilizamos la misma vista para el filtrado por fecha, por eso utilizamos un tempData
                    TempData["mostrarError"] = true; //Al no tener un index Este tempData lo utilizamos para que no muestre el mensaje de error al entrar por primera vez
                    if(numeroDocumento == null)
                    {
                        TempData["mostrarError"] = false;
                    }
                    listado = Sistema.ObtenerInstancia.ObtenerAgendaPorDocumento(tipoDocumento, numeroDocumento!);                    
                }
                catch (Exception ex)
                {
                    ViewBag.MensajeError = ex.Message;
                }
                return View(listado);
            }
            else
            {
                return RedirectToAction("NoAutorizado", "Error");
            }
        } 

        public IActionResult MostrarAgendaSegunFecha(DateTime fecha) //Comparte la View() con el Action MostrarAgendaSegunDocumento()
        {
            if (HttpContext.Session.GetString("UsuarioRol") == "OPERADOR")
            {
                List<Agenda> listado = new List<Agenda>();
                try
                {
                    TempData["tipo"] = "FECHA"; //Reutilizamos la misma vista para el filtrado por documento, por eso utilizamos un tempData
                    TempData["mostrarError"] = true; //Al no tener un index Este tempData lo utilizamos para que no muestre el mensaje de error al entrar por primera vez
                    if (fecha == DateTime.MinValue)
                    {
                        TempData["mostrarError"] = false;
                    }
                    listado = Sistema.ObtenerInstancia.ObtenerAgendaPorFecha(fecha);                    
                }
                catch (Exception ex)
                {
                    ViewBag.MensajeError = ex.Message;
                }
                return View("MostrarAgendaSegunDocumento", listado);
            }
            else
            {
                return RedirectToAction("NoAutorizado", "Error");
            }
        }

        public IActionResult MostrarAgendasPendientes() //Comparte la View() con el Action MostrarAgenda
        {
            List<Agenda> listado = null!;
            try
            {
                listado = Sistema.ObtenerInstancia.ObtenerAgendasPendientes();

                TempData["MostrarBotonConfirmar"] = true;                
            }
            catch (Exception ex)
            {
                ViewBag.MensajeError = (ex.Message);
            }
            return View("MostrarAgenda", listado); //Reutilizamos la vista de mostrar agenda.
        }

        public IActionResult ConfirmarAgenda(int elID) //Entendimos que confirmar agenda simplemente cambia su estado, los chequeos y descuentos sobre el costo final se calculan en el momento que el huesped agenda una actividad, por lo que el precio final se establece al momento de solicitar la agenda.
        {
            if (HttpContext.Session.GetString("UsuarioRol") == "OPERADOR")
            {
                try
                {
                    Agenda agenda = Sistema.ObtenerInstancia.ObtenerAgendaPorId(elID);
                    agenda.ConfirmarAgenda();
                    TempData["MensajeExito"] = $"Agenda para {agenda.Actividad.Nombre} se confirmo con exito!"; 
                }
                catch (Exception ex)
                {
                    TempData["MensajeError"] = ex.Message; //El mensaje de esta excepcion no se muestra nunca debido a que si una actividad de una agenda no tiene lugar disponibles, el boton para confirmar agenda se deshabilita, pero de todos modos controlamos la excepcion por si en algun momento se desea implementar nuevas funcionalidades que involucren este action.
                }

                return RedirectToAction("MostrarAgendasPendientes");              
            }
            else
            {
                return RedirectToAction("NoAutorizado", "Error");
            }
        }
    }
}

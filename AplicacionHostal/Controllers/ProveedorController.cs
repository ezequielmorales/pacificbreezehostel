using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace AplicacionHostal.Controllers
{
    public class ProveedorController : Controller
    {
        public IActionResult Listar()
        {
            if (HttpContext.Session.GetString("UsuarioRol") == "OPERADOR")
            {
                List<Proveedor> listado = new List<Proveedor>();

                try
                {                    
                    listado = Sistema.ObtenerInstancia.ListarProveedoresAlfabeticamente();
                }
                catch(Exception ex)
                {
                    TempData["MensajeError"] = ex.Message;
                }
                return View(listado);
            }
            else
            {
                return RedirectToAction("NoAutorizado", "Error");
            }
        }

        public IActionResult IndexEstablecerPromocion(int NumeroProveedor)
        {          
            if(TempData["NumeroProveedor"] != null)
            {
                NumeroProveedor = (int)TempData["NumeroProveedor"]!;
            }
            return View(NumeroProveedor);
        }

        public IActionResult EstablecerPromocion(int NumeroProveedor, double Descuento)
        {
            Proveedor prov = null!;
            try
            {
                prov = Sistema.ObtenerInstancia.ObtenerProveedorPorId(NumeroProveedor);
                prov.EstablecerPromocion(Descuento);
                TempData["MensajeExitoso"] = "Promocion establecida correctamente.";
                return RedirectToAction("Listar");
            }
            catch (Exception ex)
            {
                TempData["MensajeError"] = ex.Message;
            }
            TempData["NumeroProveedor"] = NumeroProveedor; //Este TempData le pasa el valor del proveedor al action IndexEstablecerPromocion, ya que si la primera vez se pone un porcentaje que no esta entre 0 y 100, el parametro de IndexEstablecerPromocion queda en 0.
            return RedirectToAction("IndexEstablecerPromocion", NumeroProveedor);
        }        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    //================HERENCIA================//
    public class ActividadTerciarizada : Actividad, IValidable
    {
        public Proveedor Proveedor { get; set; }
        public bool Confirmada { get; set; }
        public DateTime FechaConfirmacion { get; set; }

        //==============CONSTRUCTOR==============//
        public ActividadTerciarizada(string nombre, string descripcion, DateTime fecha, int cantMaxPersonas, int edadMinima, Proveedor proveedor, bool confirmada, DateTime fechaConfirmacion = new DateTime(), int costo = 0) : base(nombre, descripcion, fecha, cantMaxPersonas, edadMinima, costo)
        {
            this.Proveedor = proveedor;
            this.Confirmada = confirmada;            
        }        

        public override void Validar()
        {
            validarFecha();
            validarNombre();            
            validarDescripcion();
            validarCosto();
        }

        public override double CalcularCosto(Huesped huesped)
        {
            return this.Costo - this.Costo * this.Proveedor.Descuento / 100;
        }

        public override bool ObtenerConfirmacion()
        {
            return this.Confirmada;
        }

        public override string ObtenerTipo()
        {
            return "TERCIARIZADA";
        }

        public override string ToString()
        {
            return Proveedor.Nombre;
        }
    }
}

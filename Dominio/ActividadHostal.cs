using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ActividadHostal : Actividad, IValidable
    {
        public string Responsable { get; set; }
        public string Lugar { get; set; }
        public bool AireLibre { get; set; } = false;

        public ActividadHostal(string nombre, string descripcion, DateTime fecha, int cantMaxPersonas, int edadMinima, string responsable, string lugar, bool aireLibre, int costo = 0) : base(nombre, descripcion, fecha, cantMaxPersonas, edadMinima, costo)
        {
            this.Responsable = responsable;
            this.Lugar = lugar;
            this.AireLibre = aireLibre;
        }

        public override void Validar()
        {
            validarFecha();
            validarNombre();
            validarResponsable();
            validarDescripcion();
            validarCosto();
        }

        private void validarResponsable()
        {
            if (Responsable == null || Responsable == "")
            {
                throw new Exception("El nombre de la persona responsable no puede ser vacio.");
            }
        }

        public override string ObtenerTipo()
        {
            return "HOSTAL";
        }

        public override double CalcularCosto(Huesped huesped)
        {
            double total = this.Costo;
            if (huesped.Fidelizacion == 2)
            {
                total -= this.Costo * 0.10;
            }
            else if (huesped.Fidelizacion == 3)
            {
                total -= this.Costo * 0.15;
            }
            else if (huesped.Fidelizacion == 4)
            {
                total -= this.Costo * 0.20;
            }

            return total;
        }

        public override string ToString()
        {
            return Lugar;
        }
    }
}

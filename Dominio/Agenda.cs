using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio
{
    public class Agenda : IComparable
    {
        public int Id { get; set; } = Contador;
        public Huesped Huesped { get; set; }
        public Actividad Actividad { get; set; }
        public DateTime FechaAgenda { get; set; } = DateTime.Now;
        public string Estado { get; set; }
        public double CostoFinal { get; set; }
        public static int Contador { get; set; } = 1000;        

        public void Validar()
        {
            ValidarDisponibilidad();
            ValidarEdad();            
        }

        public void IncrementarID()
        {
            Contador++;
        }

        private void ValidarDisponibilidad()
        {
            if(this.Actividad.CantDisponible == 0)
            {
                throw new Exception("No hay lugares disponibles.");
            }
        }

        private void ValidarEdad()
        {            
            if(Huesped.FechaNacimiento.AddYears(Actividad.EdadMinima) > DateTime.Now)
            {
                throw new Exception("No cumple con la edad minima para poder participar.");
            }
        }

        public void AgregarHuesped(Huesped huesped)
        {
            this.Huesped = huesped;
        }

        public void AgregarActividad(Actividad actividad)
        {
            this.Actividad = actividad;
        }

        public void CalcularCostoFinal()
        {            
            CostoFinal = Actividad.CalcularCosto(Huesped);
        }

        public void AsignarEstado()
        {
            if (CostoFinal > 0)
            {
                Estado = "PENDIENTE_PAGO";
            }
            else
            {
                Estado = "CONFIRMADA";
            }
        }

        public void ConfirmarAgenda()
        {
            ValidarDisponibilidad();
            this.Estado = "CONFIRMADA";
            this.Actividad.CantDisponible--;
        }

        public override bool Equals(object? obj)
        {
            Agenda a = (Agenda)obj!;
            return a.Huesped.TipoDocumento == this.Huesped.TipoDocumento && a.Huesped.NroDocumento == this.Huesped.NroDocumento && a.Actividad.Id == this.Actividad.Id;
        }

        public int CompareTo(object? obj)
        {
            Agenda agenda = (Agenda)obj!;

            int resultadoFecha = this.Actividad.Fecha.CompareTo(agenda.Actividad.Fecha);

            if (resultadoFecha == 0)
            {
                return this.Actividad.Nombre.CompareTo(agenda.Actividad.Nombre);
            }

            return resultadoFecha;
        }
    }
}

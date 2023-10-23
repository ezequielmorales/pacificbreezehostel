using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public abstract class Actividad : IValidable
    {
        #region Properties
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public int CantMaxPersonas { get; set; }
        public int CantDisponible { get; set; }
        public int EdadMinima { get; set; }        
        public double Costo { get; set; }
        public static int Contador { get; set; } = 100;
        #endregion

        //===constructor con costo====//
        public Actividad(string nombre, string descripcion, DateTime fecha, int cantMaxPersonas, int edadMinima, int costo)
        {
            this.Nombre = nombre;
            this.Descripcion = descripcion;
            this.Fecha = fecha;
            this.CantMaxPersonas = cantMaxPersonas;
            this.CantDisponible = cantMaxPersonas;
            this.EdadMinima = edadMinima;            
            this.Costo = costo;

            Id = Contador;
            Contador++;
        }

        //================METODOS-ToString()================//
        public override string ToString()
        {
            return $"Id: {this.Id} \nNombre: {this.Nombre} \nDescripcion: {this.Descripcion} \nFecha: {this.Fecha} \nMaximo de personas: {this.CantMaxPersonas} \nEdad minima: {this.EdadMinima} \n============================= \n";
        }

        public abstract void Validar();        
        
        protected void validarFecha()
        {
            if(Fecha < DateTime.Now)
            {
                throw new Exception("La fecha ingresada debe ser mayor o igual que la actual.");
            }
        }

        protected void validarNombre()
        {
            if(Nombre.Length > 25 || Nombre == "" || Nombre == null) 
            {
                throw new Exception("El nombre de la actividad debe tener un maximo de 25 caracteres y no puede ser vacio.");
            }
        }

        protected void validarEdadMinima()
        {
            if(EdadMinima < 1)
            {
                throw new Exception("La edad minima de una actividad debe ser mayor a 0");
            }
        }

        protected void validarDescripcion()
        {
            if(Descripcion == "" || Descripcion == null)
            {
                throw new Exception("La descripcion no puede ser vacia.");
            }
        }
        
        protected void validarCosto()
        {
            if(Costo < 0)
            {
                throw new Exception("El costo debe ser mayor o igual a 0.");
            }
        }

        public virtual bool ObtenerConfirmacion()
        {
            return true;
        }

        public abstract double CalcularCosto(Huesped huesped);

        public abstract string ObtenerTipo();

        public override bool Equals(object? obj)
        {
            Actividad ActividadAComparar = (Actividad)obj!;
            return this.Nombre == ActividadAComparar.Nombre && this.Fecha == ActividadAComparar.Fecha;
        }
    }
}

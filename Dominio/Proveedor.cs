using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Proveedor : IValidable, IComparable
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public double Descuento { get; set; }
        public int NumeroProveedor { get; set; }
        public static int Contador { get; set; } = 100;

        public Proveedor(string nombre, string telefono, string direccion, double descuento = 0)
        {
            this.Nombre = nombre;
            this.Telefono = telefono;
            this.Direccion = direccion;
            this.Descuento = descuento;
            this.NumeroProveedor = Contador;
            Contador++;
        }

        public void Validar()
        {
            ValidarNombre();
            ValidarTelefono();
            ValidarDireccion();            
        }

        private void ValidarNombre() 
        {
            if(Nombre == null || Nombre == "")
            {
                throw new Exception("El nombre no puede ser vacio.");
            }
        }

        private void ValidarTelefono() 
        {
            if (Telefono == null || Telefono == "")
            {
                throw new Exception("El telefono no puede ser vacio.");
            }
        }

        private void ValidarDireccion() 
        {
            if (Direccion == null || Direccion == "")
            {
                throw new Exception("La direccion no puede ser vacia");
            }
        }

        public void EstablecerPromocion(double porcentajePromocion)
        {
            if(porcentajePromocion >= 0 && porcentajePromocion <= 100)
            {
                Descuento = porcentajePromocion;
            }
            else
            {
                throw new Exception("El porcentaje de promocion debe estar entre 0 y 100.");
            }
        }

        //================METODOS-ToString()================//
        public override string ToString()
        {
            return $"Numero proveedor: {this.NumeroProveedor} \nNombre: {this.Nombre} \nCelular: {this.Telefono} \nDireccion: {this.Direccion} \nDescuento: {this.Descuento}% \n============================= \n";
        }

        public override bool Equals(object? obj)
        {
            Proveedor provAComparar = (Proveedor)obj!;
            return this.Nombre == provAComparar.Nombre;
        }

        public int CompareTo(object? obj)
        {
            Proveedor prov = (Proveedor)obj!;
            return this.Nombre.CompareTo(prov.Nombre);
        }
    }
}

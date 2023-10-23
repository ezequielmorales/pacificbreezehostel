using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio
{
    public class Huesped : Usuario, IValidable
    {
        public TipoDocumento TipoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public string Habitacion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Fidelizacion { get; set; } = 1;
        public Huesped()
        {

        }

        public Huesped(string elCorreo, string laContraseña,TipoDocumento tipoDocumento, string nroDocumento, string nombre, string apellido, string habitacion, DateTime fechaNacimiento)
        {
            this.Correo = elCorreo;
            this.Contraseña = laContraseña;           
            this.TipoDocumento = tipoDocumento;
            this.NroDocumento = nroDocumento;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Habitacion = habitacion;
            this.FechaNacimiento = fechaNacimiento;            
        }

        public override string ToString()
        {
            return $"Correo: {Correo}, Contraseña: {Contraseña}, Cedula: {NroDocumento}, Nombre: {Nombre}.";
        }

        public override void Validar()
        {
            validarCorreo();
            validarContraseña();
            validarNacimiento();
            validarTipo();
            validarFidelizacion();
            if (this.TipoDocumento == TipoDocumento.CI)
            {
                comprobarDigitoVerificador();
            }
            validarNombre();
            validarHabitacion();
            validarApellido();
        }

        private void validarTipo()
        {
            if (this.TipoDocumento != TipoDocumento.CI && this.TipoDocumento != TipoDocumento.PASAPORTE && this.TipoDocumento != TipoDocumento.OTRO)
            {
                throw new Exception("El tipo de documento ingresado es incorrecto.");
            }
        }

        private void validarFidelizacion()
        {
            if (Fidelizacion <= 0 || Fidelizacion >= 5)
            {
                throw new Exception("El numero de fidelizacion es incorrecto.");
            }
        }

        private void validarNacimiento()
        {
            if(FechaNacimiento > DateTime.Now)
            {
                throw new Exception("La fecha de nacimiento debe ser menor o igual a la fecha actual.");
            }
        }

        private void validarNombre()
        {
            if(Nombre == "" || Nombre == null)
            {
                throw new Exception("El nombre no puede ser vacio.");
            }
        }

        private void validarHabitacion()
        {
            if (Habitacion == "" || Habitacion == null)
            {
                throw new Exception("El numero de habitacion no puede ser vacio.");
            }
        }

        private void validarApellido()
        {
            if (Apellido == "" || Apellido == null)
            {
                throw new Exception("El apellido no puede ser vacio.");
            }
        }

        //-----------------metodo digito verificador-----------------//
        private void comprobarDigitoVerificador()
        {            
            string CI = NroDocumento;
            if(NroDocumento.Length <= 6)
            {
                for(int i = NroDocumento.Length; i < 7; i++)
                {
                    CI = '0' + CI;
                }
            }

            int digito = 0;            
            for(int i = 0; i < 7; i++)
            {                
                digito += (int)Char.GetNumericValue("2987634"[i]) * (int)Char.GetNumericValue(CI[i]);
            }

            digito = (10 - (digito % 10)) % 10;

            if (digito != (int)Char.GetNumericValue(CI[7]))
            {
                throw new Exception("La cedula no es valida.");
            }
        }
        
        public override bool Equals(object? obj)
        {
            Huesped huespedAComparar = (Huesped)obj!;
            return this.TipoDocumento == huespedAComparar.TipoDocumento && this.NroDocumento == huespedAComparar.NroDocumento;
        }
    }
}

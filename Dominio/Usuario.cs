using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio
{
    public class Usuario : IValidable
    {
        public string ?Correo { get; set; }
        public string ?Contraseña { get; set; }
        public string ?Nombre { get; set; }
        public string ?Apellido { get; set; }
        public string ?Rol { get; set; }        

        //-----------------METODOS VALIDAR-----------------//
        //-----------------metodo validar correo-----------------//
        public virtual void Validar()
        {
            validarCorreo();
            validarContraseña();
        }

        protected void validarCorreo()
        {
            if (Correo[0] == '@' || Correo[Correo.Length - 1] == '@' || !Correo.Contains("@"))
            {
                throw new Exception("El correo ingresado es incorrecto.");
            }
        }

        //-----------------metodo validar contraseña-----------------//
        protected void validarContraseña()
        {
            if (Contraseña.Length < 8)
            {
                throw new Exception("La contraseña debe tener un minimo de 8 caracteres.");
            }
        }
    }
}

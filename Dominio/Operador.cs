using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Operador : Usuario, IValidable
    {        
        public DateTime FechaInicio { get; set; }

        public Operador(string elCorreo, string laContraseña, string elNombre, string elApellido, string elRol, DateTime fechaInicio)
        {
            this.Correo = elCorreo;
            this.Contraseña = laContraseña;
            this.Nombre = elNombre;
            this.Apellido = elApellido;            
            this.Rol = elRol;
            this.FechaInicio = fechaInicio;
        }

        public override string ToString()
        {
            return $"Correo: {Correo}, Contraseña: {Contraseña}.";
        }       
    }
}

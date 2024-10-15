using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaAutenticacion.ValueObject;

namespace SistemaAutenticacion.Entidades
{
    public class Encargado : Usuario
    {
        public Encargado(string nombre, string apellido, string email, string password) : base(nombre, apellido, email, password)
        {
            Validate();
        }

       protected Encargado() { }

        public override string Rol()
        {
            return "Encargado";
        }
    }
}

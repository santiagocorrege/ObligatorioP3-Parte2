using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaAplicacion.DTO.Usuario
{
    public class DtoUsuarioLogin
    {
        public string Email { get; set; }

        public string Password { get; set; }
        public string? Role { get; set; }
    }
}

using Gestor.LogicaAplicacion.DTO.Usuario;
using SistemaAutenticacion.Entidades;
using SistemaAutenticacion.Exceptions.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaAplicacion.Mappers
{
    internal static class MapperUsuario
    {
        internal static DtoUsuarioLogin ToDto(Usuario user)
        {
            if (user == null) throw new UsuarioException("Error el usuario no puede ser nulo");
            return new DtoUsuarioLogin()
            {
                Email = user.Email.Valor,
                Role = user.Rol()
            };
        }
    }    
}

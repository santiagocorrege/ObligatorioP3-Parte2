using Gestor.LogicaAplicacion.DTO.Usuario;
using Gestor.LogicaAplicacion.InterfacesCU.Usuario;
using Gestor.LogicaAplicacion.Mappers;
using Gestor.LogicaNegocio.InterfacesRepositorios;
using SistemaAutenticacion.Exceptions.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaAplicacion.ImplementacionCU.Usuario
{
    public class UsuarioLogin : IUsuarioLogin
    {
        private IRepositorioUsuario _repoUsuario;

        public UsuarioLogin(IRepositorioUsuario repoUsuario)
        {
            _repoUsuario = repoUsuario;
        }

        public DtoUsuarioLogin Ejecutar(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) throw new UsuarioException("El email y/o contrasena no pueden ser vacios");
            var usuario = _repoUsuario.GetByUsuarioLogin(email, password);
            if (usuario == null) throw new UsuarioException("El usuario y/o contrasena no es valido");
            return MapperUsuario.ToDto(usuario);
        }
    }
}

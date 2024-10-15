using Gestor.LogicaAplicacion.DTO.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaAplicacion.InterfacesCU.Usuario
{
    public interface IUsuarioLogin
    {
        public DtoUsuarioLogin Ejecutar(string Email, string password);
    }
}

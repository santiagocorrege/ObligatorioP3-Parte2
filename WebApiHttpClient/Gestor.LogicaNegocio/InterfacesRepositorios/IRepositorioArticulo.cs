using Gestor.LogicaNegocio.Articulos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioArticulo : IRepositorio<Articulo>
    {
        IEnumerable<Articulo> GetArticulosOrdenadosAlfabeticamente();
    }
}

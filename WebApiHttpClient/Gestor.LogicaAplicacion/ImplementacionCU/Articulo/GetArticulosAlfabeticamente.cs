using Gestor.LogicaAplicacion.DTO.Articulo;
using Gestor.LogicaAplicacion.InterfacesCU.Articulo;
using Gestor.LogicaAplicacion.Mappers;
using Gestor.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaAplicacion.ImplementacionCU.Articulo
{
    public class GetArticulosAlfabeticamente : IGetArticulosAlfabeticamente
    {
        private IRepositorioArticulo _repoArticulo;

        public GetArticulosAlfabeticamente(IRepositorioArticulo repoArticulo)
        {
            _repoArticulo = repoArticulo;
        }

        public IEnumerable<DtoArticuloCompleto> Ejecutar()
        {
            var articulos = _repoArticulo.GetArticulosOrdenadosAlfabeticamente();
            if (articulos == null) throw new ArgumentNullException("No existen articulos registrados");

            return MapperArticulo.FromLista(articulos);

        }
    }
}

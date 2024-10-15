using Gestor.LogicaAplicacion.DTO.Articulo;
using Gestor.LogicaNegocio.Articulos;
using Gestor.LogicaNegocio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaAplicacion.Mappers
{
    public class MapperArticulo
    {
        public static DtoArticuloCompleto ToDtoArticuloCompleto(Articulo articulo)
        {
            if (articulo == null) throw new ArgumentNullException("El articulo que se intenta mappear es nulo");
            return new DtoArticuloCompleto()
            {
                Id = articulo.Id,
                Nombre = articulo.Nombre,
                Descripcion = articulo.Descripcion,
                Codigo = articulo.Codigo,
                PrecioActual = articulo.PrecioActual,
            };
        }

        public static IEnumerable<DtoArticuloCompleto> FromLista(IEnumerable<Articulo> articulos)
        {
            if (articulos == null) throw new ArticuloException("No se pueden mappear los articulos");
            return articulos.Select(a => ToDtoArticuloCompleto(a));
        }
    }
}

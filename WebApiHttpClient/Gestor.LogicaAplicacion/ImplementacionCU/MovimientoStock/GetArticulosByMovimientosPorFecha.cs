using Gestor.LogicaAplicacion.DTO.Articulo;
using Gestor.LogicaAplicacion.DTO.MovimientoStock;
using Gestor.LogicaAplicacion.InterfacesCU.MovimientoStock;
using Gestor.LogicaAplicacion.Mappers;
using Gestor.LogicaNegocio.Excepciones;
using Gestor.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaAplicacion.ImplementacionCU.MovimientoStock
{
    public class GetArticulosByMovimientosPorFecha : IGetArticulosByMovimientosPorFecha
    {
        private IRepositorioMovimientoStock _repoMovimientos;

        public GetArticulosByMovimientosPorFecha(IRepositorioMovimientoStock repoMovimientos)
        {
            _repoMovimientos = repoMovimientos;
        }

        public IEnumerable<DtoArticuloCompleto> Ejecutar(DateTime fechaInicio, DateTime fechaFin, int numPagina, int cantidadRegistros)
        {
            if(fechaFin == DateTime.MinValue || fechaFin == DateTime.MinValue)
            {
                throw new MovimientoStockException("Las fechas no pueden ser nulas");
            }
            var articulos = _repoMovimientos.GetArticulosConMovimientosRangoFechas(fechaInicio, fechaFin, numPagina, cantidadRegistros);
            if(articulos == null)
            {
                throw new MovimientoStockException("No existen articulos que poseean movimientos en el rango de fechas seleccionado");
            }
            return MapperArticulo.FromLista(articulos);
        }
    }
}

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
    public class GetMovimientosStockByArticuloAndTipo : IGetMovimientosStockByArticuloAndTipo
    {
        private IRepositorioMovimientoStock _repoMovimientos;
        private IRepositorioArticulo _repoArticulos;
        private IRepositorioTipoMovimiento _repoTipos;
        public GetMovimientosStockByArticuloAndTipo(IRepositorioMovimientoStock repoMovimientos, IRepositorioArticulo repoArticulos, IRepositorioTipoMovimiento repoTipos)
        {
            _repoMovimientos = repoMovimientos;
            _repoArticulos = repoArticulos;
            _repoTipos = repoTipos;            
        }


        public IEnumerable<DtoMovimientoStockConArticuloTipoMovimiento> Ejecutar(int idArticulo, int idTipoMovimiento, int numPagina, int cantidadRegistros)
        {
            if(idArticulo == null || idArticulo == 0)
            {
                throw new MovimientoStockException("El id del articulo no puede ser nulo");
            }
            var articulo = _repoArticulos.GetById(idArticulo);
            if(articulo == null)
            {
                throw new MovimientoStockException("No existe un articulo registrado con ese id");
            }

            if (idTipoMovimiento == null || idTipoMovimiento == 0)
            {
                throw new MovimientoStockException("El id del tipo de movimiento no puede ser nulo");
            }
            var tipoMovimiento = _repoTipos.GetById(idTipoMovimiento);
            if (tipoMovimiento == null)
            {
                throw new MovimientoStockException("No existe un tipo de movimiento registrado con ese id");
            }
            var movimientos = _repoMovimientos.GetMovimientosStockByArticuloAndTipo(idArticulo, idTipoMovimiento, numPagina, cantidadRegistros);
            if(movimientos == null || movimientos.Count() == 0)
            {
                throw new MovimientoStockException("No existen movimientos de stock para ese articulo con ese tipo de movimiento");
            }
            return MapperMovimientoStock.FromListConArticuloYTipo(movimientos);

        }
    }
}

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
    public class GetMovimientoStock : IGetMovimientoStock
    {
        private IRepositorioMovimientoStock _repoMovimientoStock;
        private IRepositorioParametro _repoParametro;

        public GetMovimientoStock(IRepositorioMovimientoStock repoMovimientoStock, IRepositorioParametro repoParametros)
        {
            _repoMovimientoStock = repoMovimientoStock;
            _repoParametro = repoParametros;
        }

        public int ParametroTopePaginas()
        {
            int tope = _repoParametro.GetValorInt("TopePaginas");
            return tope;
        }


        public IEnumerable<DtoMovimientoIndex> GetAll()
        {
            var movimientos = _repoMovimientoStock.GetAll();
            if(movimientos == null || movimientos.Count() == 0)
            {
                throw new MovimientoStockException("No existen movimientos registrados");
            }
            return MapperMovimientoStock.FromListIndex(movimientos);
        }

        public DtoMovimientoStockAdd GetById(int id)
        {
            if (id == null || id == 0)
            {
                throw new MovimientoStockException("El id del movimientos que desea buscar no puede ser nulo");
            }
            var movimiento = _repoMovimientoStock.GetById(id);
            if (movimiento == null) throw new MovimientoStockException("No existe un movimientos con ese id");
            return MapperMovimientoStock.ToDtoAdd(movimiento);
        }

        public IEnumerable<dynamic> GetMovimientosSumarry()
        {
            var movimientos = _repoMovimientoStock.GetMovimientosCantidadesByTipoAndAno();
            if (!movimientos.Any())
            {
                throw new MovimientoStockException("No existen movimientos registrados");
            }
            return movimientos;
        }
    }
}

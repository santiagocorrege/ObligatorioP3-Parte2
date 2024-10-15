using Gestor.LogicaAplicacion.DTO.MovimientoStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaAplicacion.InterfacesCU.MovimientoStock
{
    public interface IGetMovimientoStock
    {
        public IEnumerable<DtoMovimientoIndex> GetAll();

        public DtoMovimientoStockAdd GetById(int id);

        public IEnumerable<dynamic> GetMovimientosSumarry();

        public int ParametroTopePaginas();
    }
}

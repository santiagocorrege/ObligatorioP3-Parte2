using Gestor.LogicaAplicacion.DTO.MovimientoStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaAplicacion.InterfacesCU.MovimientoStock
{
    public interface IGetMovimientosStockByArticuloAndTipo
    {
        IEnumerable<DtoMovimientoStockConArticuloTipoMovimiento> Ejecutar(int idArticulo, int idTipoMovimiento, int numeroPagina, int cantidadElementos);
    }
}

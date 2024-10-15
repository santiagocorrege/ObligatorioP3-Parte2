using Gestor.LogicaAplicacion.DTO.MovimientoStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaAplicacion.InterfacesCU.MovimientoStock
{
    public interface IAddMovimientoStock
    {
        public int Ejecutar(DtoMovimientoStockAdd movimiento);
    }
}

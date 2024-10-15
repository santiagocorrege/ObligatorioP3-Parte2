using Gestor.LogicaAplicacion.DTO.Articulo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaAplicacion.InterfacesCU.MovimientoStock
{
    public interface IGetArticulosByMovimientosPorFecha
    {
        public IEnumerable<DtoArticuloCompleto> Ejecutar(DateTime fechaInicio, DateTime fechaFin, int numPagina, int cantidadRegistros);
    }
}

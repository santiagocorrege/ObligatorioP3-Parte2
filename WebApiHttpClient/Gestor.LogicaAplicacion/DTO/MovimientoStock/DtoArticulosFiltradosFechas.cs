using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaAplicacion.DTO.MovimientoStock
{
    public class DtoArticulosFiltradosFechas
    {
        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public int Pagina { get; set; }
    }
}

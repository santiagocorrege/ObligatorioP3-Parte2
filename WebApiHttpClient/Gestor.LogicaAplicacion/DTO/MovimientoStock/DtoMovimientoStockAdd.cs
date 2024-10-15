using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaAplicacion.DTO.MovimientoStock
{
    public class DtoMovimientoStockAdd
    {        
        [Required]
        public int ArticuloId { get; init; }

        [Required]
        public int TipoMovimientoId { get; init; }

        [Required]
        public int Cantidad { get; init; }
    }
}

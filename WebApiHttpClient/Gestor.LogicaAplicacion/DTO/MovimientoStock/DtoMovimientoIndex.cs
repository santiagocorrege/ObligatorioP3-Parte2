using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaAplicacion.DTO.MovimientoStock
{
    public class DtoMovimientoIndex
    {
        public string NombreArticulo { get; set; }

        [Required]
        public int ArticuloId { get; init; }

        public string NombreTipoMovimiento { get; set; }

        [Required]
        public int TipoMovimientoId { get; init; }

        [Required]
        public int Cantidad { get; init; }
    }
}

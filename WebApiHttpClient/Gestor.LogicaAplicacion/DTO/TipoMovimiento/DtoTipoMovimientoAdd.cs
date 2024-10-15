using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaAplicacion.DTO.TipoMovimiento
{
    public class DtoTipoMovimientoAdd
    {       

        [Required]
        public string Nombre { get; set; }

        [Required]
        public int Coeficiente { get; set; }
    }
}

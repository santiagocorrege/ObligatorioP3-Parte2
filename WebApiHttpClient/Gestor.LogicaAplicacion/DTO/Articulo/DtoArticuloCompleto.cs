using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaAplicacion.DTO.Articulo
{
    public class DtoArticuloCompleto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public string Codigo { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioActual { get; set; }

    }
}

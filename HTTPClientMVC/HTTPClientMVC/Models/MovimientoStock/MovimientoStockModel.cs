using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HTTPClientMVC.Models.MovimientoStock
{
    public class MovimientoStockModel
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

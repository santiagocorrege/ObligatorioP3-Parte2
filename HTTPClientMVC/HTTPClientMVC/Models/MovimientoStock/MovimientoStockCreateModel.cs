using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HTTPClientMVC.Models.MovimientoStock
{
    public class MovimientoStockCreateModel
    {
        [Required(ErrorMessage = "El campo Artículo es requerido.")]
        public int SelectedArticuloId { get; set; }

        [Required(ErrorMessage = "El campo Tipo de Movimiento es requerido.")]
        public int SelectedTipoMovimientoId { get; set; }

        [Required(ErrorMessage = "El campo Cantidad es requerido.")]
        public int Cantidad { get; set; }
        public SelectList ArticulosSelectList { get; set; }

        public SelectList TipoMovimientoSelectList { get; set; }

        public IEnumerable<MovimientoStockConArticuloTipoMovimientoModel> ListaResultadosMovimientos {  get; set; }

        public int Pagina { get; set; }

    }
}

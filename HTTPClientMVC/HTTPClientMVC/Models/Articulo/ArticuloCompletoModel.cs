using System.ComponentModel.DataAnnotations.Schema;

namespace HTTPClientMVC.Models.Articulo
{
    public class ArticuloCompletoModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }

        public decimal PrecioActual { get; set; }
    }
}

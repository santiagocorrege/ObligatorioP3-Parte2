namespace HTTPClientMVC.Models.Articulo
{
    public class ArticuloFiltradoModel
    {
        public IEnumerable<ArticuloCompletoModel> ListaArticulos { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin {  get; set; }

        public int Pagina { get; set; }
    }
}

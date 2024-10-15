using Gestor.LogicaNegocio.Articulos;
using Gestor.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioMovimientoStock       
    {
        MovimientoStock GetById(int id);
        void Add(MovimientoStock movimientoStock);
        IEnumerable<MovimientoStock> GetAll();

        IEnumerable<MovimientoStock> GetMovimientosStockByArticuloAndTipo(int idArticulo, int idTipoMovimiento, int numPagina, int cantidadRegistros);

        IEnumerable<Articulo> GetArticulosConMovimientosRangoFechas(DateTime fechaInicio, DateTime fechaFin, int numPagina, int cantidadRegistros);
        IEnumerable<dynamic> GetMovimientosCantidadesByTipoAndAno();
    }
}

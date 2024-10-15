using Gestor.LogicaAplicacion.DTO.MovimientoStock;
using Gestor.LogicaAplicacion.InterfacesCU.MovimientoStock;
using Gestor.LogicaAplicacion.Mappers;
using Gestor.LogicaNegocio.Excepciones;
using Gestor.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaAplicacion.ImplementacionCU.MovimientoStock
{
    public class AddMovimientoStock : IAddMovimientoStock
    {
        private IRepositorioMovimientoStock _repoMovimientoStock;
        private IRepositorioArticulo _repoArticulo;
        private IRepositorioTipoMovimiento _repoTipoMovimiento;
        private IRepositorioParametro _repoParametros;

        public AddMovimientoStock(IRepositorioMovimientoStock repoMovimientoStock, IRepositorioTipoMovimiento repoTipoMovimiento, IRepositorioArticulo repoArticulo, IRepositorioParametro repoParametros)
        {
            _repoMovimientoStock = repoMovimientoStock;
            _repoTipoMovimiento = repoTipoMovimiento;
            _repoArticulo = repoArticulo;
            _repoParametros = repoParametros;
        }

        public int Ejecutar(DtoMovimientoStockAdd dto)
        {
            try
            {
                //Doble comprobacion de cantidad tambien existe en la validacion IValidate de Movimiento Stock
                if (dto == null || dto.Cantidad <= 0)
                {
                    throw new MovimientoStockException("El movimiento de stock no puede ser nulo ni tampoco la cantidad menor o igual a 0");
                }
                LogicaNegocio.Entidades.MovimientoStock movimientoStock = MapperMovimientoStock.FromDtoAdd(dto);
                if(movimientoStock == null)
                {
                    throw new MovimientoStockException("El movimiento de stock no puede ser nulo, problema mappeo");
                }
                var articulo = _repoArticulo.GetById(movimientoStock.ArticuloId);
                if(articulo == null)
                {
                    throw new MovimientoStockException("El articulo del movimiento de stock no puede ser nulo");
                }
                var tipoMovimiento = _repoTipoMovimiento.GetById(movimientoStock.TipoMovimientoId);
                if (tipoMovimiento == null)
                {
                    throw new MovimientoStockException("El tipo de movimiento del movimiento de stock no puede ser nulo");
                }
                LogicaNegocio.Entidades.MovimientoStock.s_TopeCantidad =  _repoParametros.GetValorInt("TopeCantidad");
                //Verifica que la cantidad no supere el tope 
                movimientoStock.VerificarTope();
                movimientoStock.TipoMovimiento = tipoMovimiento;
                movimientoStock.Articulo = articulo;
                _repoMovimientoStock.Add(movimientoStock);
                return movimientoStock.Id;

            } 
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

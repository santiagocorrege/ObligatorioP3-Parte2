using Gestor.LogicaAplicacion.DTO.MovimientoStock;
using Gestor.LogicaNegocio.Entidades;
using Gestor.LogicaNegocio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaAplicacion.Mappers
{
    internal static class MapperMovimientoStock
    {
        internal static MovimientoStock FromDtoAdd(DtoMovimientoStockAdd dto)
        {
            if (dto == null) throw new MovimientoStockException("El movimiento de stock que desea agregar no puede ser nulo");
            LogicaNegocio.Entidades.MovimientoStock movimiento = new MovimientoStock(dto.ArticuloId, dto.TipoMovimientoId, dto.Cantidad);
            if(movimiento == null) throw new MovimientoStockException("El movimiento de stock que desea agregar no puede ser nulo");
            return movimiento;
        }

        internal static DtoMovimientoStockAdd ToDtoAdd(MovimientoStock movimiento)
        {
            if (movimiento == null) throw new MovimientoStockException("El movimiento de stock que desea listar no puede ser nulo");
            var dto = new DtoMovimientoStockAdd()
            {                
                Cantidad = movimiento.Cantidad,
                ArticuloId = movimiento.ArticuloId,
                TipoMovimientoId = movimiento.TipoMovimientoId
            };
            return dto;
        }

        internal static IEnumerable<DtoMovimientoStockAdd> FromList(IEnumerable<MovimientoStock> movimientos)
        {
            if (movimientos == null || movimientos.Count() == 0) throw new MovimientoStockException("No existen registros para ser mappeados de movimientos stock");
            return movimientos.Select(m => ToDtoAdd(m));
        }        


        internal static DtoMovimientoIndex ToDtoIndex(MovimientoStock movimiento)
        {
            if (movimiento == null) throw new MovimientoStockException("El movimiento de stock que desea listar no puede ser nulo");
            var dto = new DtoMovimientoIndex()
            {
                Cantidad = movimiento.Cantidad,
                ArticuloId = movimiento.ArticuloId,
                TipoMovimientoId = movimiento.TipoMovimientoId,
                NombreArticulo = movimiento.Articulo.Nombre,
                NombreTipoMovimiento = movimiento.TipoMovimiento.Nombre
            };
            return dto;
        }

        internal static IEnumerable<DtoMovimientoIndex> FromListIndex(IEnumerable<MovimientoStock> movimientos)
        {
            if (movimientos == null || movimientos.Count() == 0) throw new MovimientoStockException("No existen registros para ser mappeados de movimientos stock");
            return movimientos.Select(m => ToDtoIndex(m));
        }

        //Para DtoMovimientoStockConArticuloTipoMovimiento
        internal static DtoMovimientoStockConArticuloTipoMovimiento ToDtoMovimientoStockConArticuloTipoMovimiento(MovimientoStock movimiento)
        {
            if (movimiento == null) throw new MovimientoStockException("El movimiento de stock que desea listar no puede ser nulo");
            var dto = new DtoMovimientoStockConArticuloTipoMovimiento()
            {
                Id = movimiento.Id,
                FechaMovimiento = movimiento.FechaMovimiento,
                Cantidad = movimiento.Cantidad,
                IdArticulo = movimiento.Articulo.Id,
                NombreArticulo = movimiento.Articulo.Nombre,    
                PrecioActualArticulo = movimiento.Articulo.PrecioActual,
                IdTipoMovimiento = movimiento.TipoMovimiento.Id,
                NombreTipoMovimiento = movimiento.TipoMovimiento.Nombre,
                CoeficienteTipoMovimiento = movimiento.TipoMovimiento.Coeficiente
            };
            return dto;
        }
        internal static IEnumerable<DtoMovimientoStockConArticuloTipoMovimiento> FromListConArticuloYTipo(IEnumerable<MovimientoStock> movimientos)
        {
            if (movimientos == null || movimientos.Count() == 0) throw new MovimientoStockException("No existen registros para ser mappeados de movimientos stock");
            return movimientos.Select(m => ToDtoMovimientoStockConArticuloTipoMovimiento(m));
        }
    }
}

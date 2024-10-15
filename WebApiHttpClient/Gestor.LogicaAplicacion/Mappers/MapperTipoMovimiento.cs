using Gestor.LogicaAplicacion.DTO.TipoMovimiento;
using Gestor.LogicaNegocio.Articulos;
using Gestor.LogicaNegocio.Entidades;
using Gestor.LogicaNegocio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaAplicacion.Mappers
{
    internal static class MapperTipoMovimiento
    {
        //ADD
        internal static TipoMovimiento FromDtoAdd(DtoTipoMovimientoAdd dto)
        {
            if (dto == null) throw new TipoMovimientoException("El dto a mapper no puede ser nulo");
            TipoMovimiento tipoMovimiento = new TipoMovimiento(dto.Nombre, dto.Coeficiente);
            return tipoMovimiento;
        }

        //GET 
        internal static DtoTipoMovimientoCompleto ToDtoCompleto(TipoMovimiento tipoMovimiento)
        {
            if (tipoMovimiento == null) throw new TipoMovimientoException("El tipo de movimiento que se desea mappear no puede ser nulo");
            var dtoTipoMovimiento = new DtoTipoMovimientoCompleto() {
                Id = tipoMovimiento.Id,
                Nombre = tipoMovimiento.Nombre,
                Coeficiente = tipoMovimiento.Coeficiente
            };
            return dtoTipoMovimiento;
        }

        internal static TipoMovimiento FromDtoCompleto(DtoTipoMovimientoCompleto dto)
        {
            if (dto == null) throw new TipoMovimientoException("El tipo de movimiento que se desea mappear no puede ser nulo");
            TipoMovimiento tipoMovimiento = new TipoMovimiento(dto.Nombre, dto.Coeficiente);
            return tipoMovimiento;
        }

        internal static IEnumerable<DtoTipoMovimientoCompleto> FromLista (IEnumerable<TipoMovimiento> tipoMovimientos)
        {
            if (tipoMovimientos == null) throw new TipoMovimientoException("No se pueden mappear los tipo de movimientos");
            return tipoMovimientos.Select(t => ToDtoCompleto(t));
        }
    }
}

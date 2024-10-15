using Gestor.LogicaAplicacion.DTO.TipoMovimiento;
using Gestor.LogicaAplicacion.InterfacesCU.TipoMovimiento;
using Gestor.LogicaAplicacion.Mappers;
using Gestor.LogicaNegocio.Excepciones;
using Gestor.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaAplicacion.ImplementacionCU.TipoMovimiento
{
    public class GetTipoMovimiento : IGetTipoMovimiento
    {
        private IRepositorioTipoMovimiento _repoTipoMovimiento;

        public GetTipoMovimiento(IRepositorioTipoMovimiento repoTipoMovimiento)
        {
            _repoTipoMovimiento = repoTipoMovimiento;
        }

        public IEnumerable<DtoTipoMovimientoCompleto> GetAll()
        {
            var tiposMovimientos = _repoTipoMovimiento.GetAll();
            if(!tiposMovimientos.Any())
            {
                throw new TipoMovimientoException("No hay tipos de movimientos registrados");
            }
            return MapperTipoMovimiento.FromLista(tiposMovimientos);
        }

        public DtoTipoMovimientoCompleto GetById(int id)
        {
            if(id == 0) { throw new TipoMovimientoException("El id no puede ser nulo"); }
            var tipoMovimiento = _repoTipoMovimiento.GetById(id);
            if(tipoMovimiento == null) { throw new TipoMovimientoException("No existe un tipo de movimiento registrado con ese id"); }

            return MapperTipoMovimiento.ToDtoCompleto(tipoMovimiento);
        }
    }
}

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
    public class UpdateTipoMovimiento : IUpdateTipoMovimiento
    {
        private IRepositorioTipoMovimiento _repoTipoMovimiento;

        public UpdateTipoMovimiento(IRepositorioTipoMovimiento repoTipoMovimiento)
        {
            _repoTipoMovimiento = repoTipoMovimiento;
        }

        public void Ejecutar(int id, DtoTipoMovimientoCompleto dto)
        {
            if (id == null)
            {
                throw new TipoMovimientoException("El id del tipo de movimiento no es valido");
            }
            if(dto == null)
            {
                throw new TipoMovimientoException("Faltan datos para actualizar el tipo de movimiento");
            }
            Gestor.LogicaNegocio.Entidades.TipoMovimiento tipoMovimiento = MapperTipoMovimiento.FromDtoCompleto(dto);
            _repoTipoMovimiento.Update(id, tipoMovimiento);            
        }
    }
}

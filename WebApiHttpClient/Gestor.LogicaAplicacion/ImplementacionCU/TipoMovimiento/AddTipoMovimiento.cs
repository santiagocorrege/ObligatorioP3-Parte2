using Gestor.LogicaAplicacion.DTO.TipoMovimiento;
using Gestor.LogicaAplicacion.InterfacesCU.TipoMovimiento;
using Gestor.LogicaAplicacion.Mappers;
using Gestor.LogicaNegocio.Articulos;
using Gestor.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaAplicacion.ImplementacionCU.TipoMovimiento
{
    public class AddTipoMovimiento : IAddTipoMovimiento
    {
        private IRepositorioTipoMovimiento _repoTipoMovimiento;

        public AddTipoMovimiento(IRepositorioTipoMovimiento repo)
        {
            _repoTipoMovimiento = repo;
        }

        public int Ejecutar(DtoTipoMovimientoAdd dto)
        {
            if(dto == null)
            {
                throw new Exception("El tipo de movimiento no puede ser nulo");
            }
            Gestor.LogicaNegocio.Entidades.TipoMovimiento tMovimiento = MapperTipoMovimiento.FromDtoAdd(dto);
            _repoTipoMovimiento.Add(tMovimiento);
            return tMovimiento.Id;
        }
    }
}

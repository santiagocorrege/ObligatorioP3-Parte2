using Gestor.LogicaAplicacion.DTO.TipoMovimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaAplicacion.InterfacesCU.TipoMovimiento
{
    public interface IGetTipoMovimiento
    {
        public IEnumerable<DtoTipoMovimientoCompleto> GetAll();

        public DtoTipoMovimientoCompleto GetById(int id);

    }
}

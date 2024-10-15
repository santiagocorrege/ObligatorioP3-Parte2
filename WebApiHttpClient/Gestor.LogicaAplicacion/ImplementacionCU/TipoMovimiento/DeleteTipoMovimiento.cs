using Gestor.LogicaAplicacion.InterfacesCU.TipoMovimiento;
using Gestor.LogicaNegocio.Excepciones;
using Gestor.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaAplicacion.ImplementacionCU.TipoMovimiento
{
    public class DeleteTipoMovimiento : IDeleteTipoMovimiento
    {
        private IRepositorioTipoMovimiento _repoTipoMovimiento;

        public DeleteTipoMovimiento(IRepositorioTipoMovimiento repoTipoMovimiento)
        {
            _repoTipoMovimiento = repoTipoMovimiento;
        }

        public void Ejecutar(int id)
        {
            if (id == 0 || id == null) throw new TipoMovimientoException("El id no es valido");
            _repoTipoMovimiento.Remove(id);
        }
    }
}

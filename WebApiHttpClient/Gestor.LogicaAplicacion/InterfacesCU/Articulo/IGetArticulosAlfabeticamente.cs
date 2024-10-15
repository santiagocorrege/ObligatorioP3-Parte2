using Gestor.LogicaAplicacion.DTO.Articulo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaAplicacion.InterfacesCU.Articulo
{
    public interface IGetArticulosAlfabeticamente
    {
        public IEnumerable<DtoArticuloCompleto> Ejecutar();
    }
}

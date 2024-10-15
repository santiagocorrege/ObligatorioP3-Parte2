using Gestor.LogicaNegocio.Excepciones;
using Gestor.LogicaNegocio.InterfacesEntidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaNegocio.Entidades
{
    [Index(nameof(Nombre), IsUnique = true)]
    public class TipoMovimiento : IEntity, IValidate
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        //Solo puede ser 1 o -1
        public int Coeficiente { get; set; }

        protected TipoMovimiento() { }

        public TipoMovimiento(string nombre,  int coeficiente)
        {
            Nombre = nombre;
            Coeficiente = coeficiente;
            Validate();
        }
        public void Validate()
        {
            if (String.IsNullOrEmpty(Nombre))
            {
                throw new TipoMovimientoException("El nombre no puede ser nulo");
            }
            if(Coeficiente != 1 && Coeficiente != -1)
            {
                throw new TipoMovimientoException("Invalido tipo de movimiento el movimiento puede ser agregando o quitando stock unicamente");
            }
        }

        public void Modificar (TipoMovimiento tipoMovimientoModificado)
        {
            Nombre = tipoMovimientoModificado.Nombre;
            Coeficiente = tipoMovimientoModificado.Coeficiente;
        }
    }
}

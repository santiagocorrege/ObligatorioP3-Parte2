using Gestor.LogicaNegocio.Articulos;
using Gestor.LogicaNegocio.Excepciones;
using Gestor.LogicaNegocio.InterfacesEntidades;
using SistemaAutenticacion.InterfacesEntidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IEntity = Gestor.LogicaNegocio.InterfacesEntidades.IEntity;
using IValidate = Gestor.LogicaNegocio.InterfacesEntidades.IValidate;

namespace Gestor.LogicaNegocio.Entidades
{
    public class MovimientoStock : IEntity, IValidate
    {
        public int Id { get; set; }

        public Articulo Articulo { get; set; }

        [ForeignKey(nameof(Articulo))]
        public int ArticuloId { get; init; }

        public TipoMovimiento TipoMovimiento { get; set; }

        [ForeignKey(nameof(TipoMovimiento))]
        public int TipoMovimientoId { get; init; }

        public int Cantidad { get; init; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime FechaMovimiento { get; init; }

        public static int s_TopeCantidad {get; set;}

        protected MovimientoStock() { }

        public MovimientoStock(int articuloId, int tipoMovimientoId, int cantidad)
        {
            FechaMovimiento = DateTime.Now;            
            ArticuloId = articuloId;
            TipoMovimientoId = tipoMovimientoId;
            Cantidad = cantidad;
            Validate();
        }

        public void Validate()
        {
            if (Articulo == null && ArticuloId == 0)
            {
                throw new MovimientoStockException("El cliente no puede ser nulo");
            }

            if (FechaMovimiento < DateTime.MinValue)
            {
                throw new MovimientoStockException("La fecha de entrega no es valida");
            }
            if(Cantidad <= 0)
            {
                throw new MovimientoStockException("La cantidad debe ser superior a 0");
            }
        }

        public void VerificarTope()
        {
            if(Cantidad > s_TopeCantidad)
            {
                throw new MovimientoStockException("La cantidad de unidades del movimiento de stock no puede superar el tope (1500)");
            }
        }
    }
}

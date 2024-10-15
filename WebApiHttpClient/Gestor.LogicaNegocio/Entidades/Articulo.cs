using Gestor.LogicaNegocio.InterfacesEntidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Gestor.LogicaNegocio.Excepciones;
using Gestor.LogicaNegocio.Entidades;

namespace Gestor.LogicaNegocio.Articulos
{
    [Index(nameof(Nombre), IsUnique = true)]
    [Index(nameof(Codigo), IsUnique = true)]
    public class Articulo : IEntity, IValidate
    {
        #region Properties
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public string Codigo { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioActual { get; set; }

        public List<MovimientoStock> HistorialMovimientos { get; set; }

        public Articulo(string nombre, string descripcion, string codigo, decimal precioActual)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Codigo = codigo;
            PrecioActual = precioActual;           
            Validate();
        }

        protected Articulo() { }
        #endregion
        #region Methods
        public void Modificar(Articulo articuloModificado)
        {
            //No hago validacion de articulo porque ese se realiza en el constructor
            Nombre = articuloModificado.Nombre;
            Descripcion = articuloModificado.Descripcion;
            Codigo = articuloModificado.Codigo;
            PrecioActual = articuloModificado.PrecioActual;
            Validate();
        }
        public void Validate()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new ArticuloException("El nombre no puede ser nulo");
            }
            if (string.IsNullOrEmpty(Descripcion))
            {
                throw new ArticuloException("La descripcion no puede ser menor a 5 caracteres");
            }
            if (Nombre.Length < 10 || Nombre.Length > 200)
            {
                throw new ArticuloException("El nombre debe tener entre 10 y 200 caracteres");
            }
            if (Descripcion.Length < 5)
            {
                throw new ArgumentException("La descripcion debe tener al menos 5 caracteres");
            }
            if (Codigo == null || Codigo.Length != 13)
            {
                throw new ArgumentException("El codigo debe tener 13 digitos");
            }
            if (PrecioActual <= 0)
            {
                throw new ArgumentException("El precio actual no es valido");
            }
        }

        public override string ToString()
        {
            return $"Id: {Id}\nNombre: {Nombre}\nDescripcion: {Descripcion}\nCodigo: {Codigo}\nPrecio Actual: {PrecioActual}";
        }
        #endregion
    }
}
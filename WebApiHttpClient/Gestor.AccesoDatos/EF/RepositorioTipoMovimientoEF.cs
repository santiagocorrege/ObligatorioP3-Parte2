using Gestor.LogicaNegocio.Articulos;
using Gestor.LogicaNegocio.Entidades;
using Gestor.LogicaNegocio.Excepciones;
using Gestor.LogicaNegocio.InterfacesRepositorios;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.AccesoDatos.EF
{
    public class RepositorioTipoMovimientoEF : IRepositorioTipoMovimiento
    {
        private GestorContext _db;

        public RepositorioTipoMovimientoEF(GestorContext db)
        {
            _db = db;
        }

        public void Add(TipoMovimiento tipoMovimiento)
        {
            try
            {
                _db.TiposMovimientos.Add(tipoMovimiento);
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    SqlException exSql = ex.InnerException as SqlException;
                    if (exSql.Number == 2601)
                        throw new TipoMovimientoException("Ya existe un tipo de movimiento con ese nombre");
                }
                throw;
            }
            catch (Exception ex)
            {
                throw new TipoMovimientoException($"El tipo de movimiento no se pudo agregar. Más info: {ex.Message}");
            }
        }

        public IEnumerable<TipoMovimiento> GetAll()
        {
            try
            {
                return _db.TiposMovimientos.ToList();
            }
            catch (Exception ex)
            {
                throw new TipoMovimientoException($"No se pudo devolver la lista de tipos de movimientos. Más info: {ex.Message}");
            }
        }

        public TipoMovimiento GetById(int id)
        {
            try
            {
                var tipoMovimiento = _db.TiposMovimientos.Find(id);
                if (tipoMovimiento == null)
                {
                    throw new TipoMovimientoException("El tipo de movimiento con ese id no existe");
                }
                return tipoMovimiento;
            }
            catch (Exception e)
            {
                throw new TipoMovimientoException($"No se ha podido encontrar el articulo con id {id}. Mas info: {e.Message}");
            }
        }

        public void Remove(int id)
        {
            try
            {
                TipoMovimiento tipoMovimiento = _db.TiposMovimientos.Find(id);
                if (tipoMovimiento == null)
                {
                    throw new TipoMovimientoException($"No existe un tipo de movimiento con id:{id}.");
                }
                var movimientosConTipoMovimiento = _db.MovimientosStock.Where(m => m.TipoMovimiento.Id == tipoMovimiento.Id).ToList();
                if (movimientosConTipoMovimiento.Any())
                {
                    throw new TipoMovimientoException($"Existen movimientos registrados con ese tipo de movimiento, no se puede eliminar de la base datos.");
                }
                _db.TiposMovimientos.Remove(tipoMovimiento);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new TipoMovimientoException($"Error. Mas info: {e.Message}");
            }
        }

        public void Remove(TipoMovimiento tipoMovimiento)
        {
            try
            {
                if (tipoMovimiento == null)
                {
                    throw new ArgumentNullException("El tipo de movimiento no puede ser nulo");
                }
                _db.TiposMovimientos.Remove(tipoMovimiento);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new TipoMovimientoException($"No eliminar el tipo de movimiento {tipoMovimiento.Nombre} de la lista. Más info: {ex.Message}");
            }
        }

        public void Update(int id, TipoMovimiento tipoMovimientoModificado)
        {
            try
            {
                var tipoMovimiento = _db.TiposMovimientos.Find(id);
                if (tipoMovimiento == null)
                {
                    throw new TipoMovimientoException($"No existe el tipo de movimiento con el id {id}");
                }
                tipoMovimiento.Modificar(tipoMovimientoModificado);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new TipoMovimientoException($"No se ha podido modificar el tipo de movimiento con id: {id}. Mas info: {ex.Message}");
            }
        }
    }
}

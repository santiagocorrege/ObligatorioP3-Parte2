using Gestor.LogicaNegocio.Articulos;
using Gestor.LogicaNegocio.Entidades;
using Gestor.LogicaNegocio.Excepciones;
using Gestor.LogicaNegocio.InterfacesRepositorios;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.AccesoDatos.EF
{
    public class RepositorioMovimientoStockEF : IRepositorioMovimientoStock
    {
        private GestorContext _db;
        private static int _cantMovimientos = 0;

        public RepositorioMovimientoStockEF(GestorContext db)
        {
            _db = db;
        }

        public void Add(MovimientoStock movimientoStock)
        {
            try
            {
                if(movimientoStock == null) { throw new MovimientoStockException("El movimiento que desea agregar no puede ser nulo"); }
                _db.Entry(movimientoStock.Articulo).State = EntityState.Unchanged;
                _db.Entry(movimientoStock.TipoMovimiento).State = EntityState.Unchanged;
                _db.MovimientosStock.Add(movimientoStock);
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    SqlException exSql = ex.InnerException as SqlException;
                    if (exSql.Number == 2601)
                        throw new Exception("Ya existe un movimiento stock con ese nombre o codigo");
                }
                throw;
            }
            catch (Exception ex)
            {
                throw new ArticuloException($"El articulo no se pudo agregar. Más info: {ex.Message}");
            }
        }

        public IEnumerable<MovimientoStock> GetAll()
        {
            try
            {
                var movimientosStock = _db.MovimientosStock
                    .Include(m => m.Articulo)
                    .Include(m => m.TipoMovimiento)
                    .ToList();
                if (movimientosStock == null || movimientosStock.Count == 0)
                {
                    throw new MovimientoStockException("No  existen movimientos stock registrados");
                }
                return movimientosStock;
            }            
            catch(Exception ex)
            {
                throw;
            }
        }

        public MovimientoStock GetById(int id)
        {
            try
            {
                if (id == null || id == 0) throw new MovimientoStockException("El id del movimientos stock que desea buscar no es valido");
                var movimientoStock = _db.MovimientosStock.FirstOrDefault(m => m.Id == id);
                if (movimientoStock == null) throw new MovimientoStockException("El movimientos stock que esta buscando no existe");
                return movimientoStock;
            }
            catch (Exception ex)
            {
                throw new MovimientoStockException("No existe un movimientos stock con ese id");
            }
        }

        public IEnumerable<MovimientoStock> GetMovimientosStockByArticuloAndTipo(int idArticulo, int idTipoMovimiento, int numPagina, int cantidadRegistros)
        {
            try
            {
                if(idArticulo == null || idArticulo == 0) { throw new MovimientoStockException("El id del articulo no puede ser nulo"); }
                if(idTipoMovimiento == null || idArticulo == 0) { throw new MovimientoStockException("El id del tipo de movimiento no puede ser nulo"); }
                var movimientosStock = _db.MovimientosStock
                    .Include(m => m.Articulo)
                    .Include(m => m.TipoMovimiento)
                    .Where(m => m.ArticuloId == idArticulo && m.TipoMovimientoId == idTipoMovimiento)
                    .OrderByDescending(m => m.FechaMovimiento)
                    .ThenBy(m => m.Cantidad)
                    .ToList();

                if (numPagina <= 1)
                {
                    numPagina = 1;
                    _cantMovimientos = movimientosStock.Count();
                }

                int numRegistrosAnteriores = cantidadRegistros * (numPagina - 1);
                movimientosStock = movimientosStock
                    .Skip(numRegistrosAnteriores)
                    .Take(cantidadRegistros)
                    .ToList();

                if (!movimientosStock.Any())
                    _cantMovimientos = 0;

                if (movimientosStock == null || movimientosStock.Count() == 0)
                {
                    throw new MovimientoStockException("No existen movimientos de stock para ese articulo con ese tipo de movimiento");
                }
                return movimientosStock;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Articulo> GetArticulosConMovimientosRangoFechas(DateTime fechaInicio, DateTime fechaFin, int numPagina, int cantidadRegistros)
        {
            try
            {                
                if (fechaInicio == null || fechaFin == null) { throw new MovimientoStockException("Las fechas no pueden ser nulas"); }

                var movimientosStock = _db.MovimientosStock
                    .Where(m => m.FechaMovimiento >= fechaInicio && m.FechaMovimiento <= fechaFin)
                    .Select(m => m.Articulo)
                    .Distinct()
                    .ToList();
                if (numPagina <= 1)
                {                    
                    _cantMovimientos = movimientosStock.Count();
                }
                    
                int numRegistrosAnteriores = cantidadRegistros * (numPagina - 1);
                if(numRegistrosAnteriores < 0)
                {
                    numRegistrosAnteriores = 0;
                }
                movimientosStock = 
                    movimientosStock
                    .Skip(numRegistrosAnteriores)
                    .Take(cantidadRegistros)
                    .ToList();

                if (!movimientosStock.Any())
                    _cantMovimientos = 0;

                if (movimientosStock == null || movimientosStock.Count() == 0)
                {
                    throw new MovimientoStockException("No existen articulos que posean movimientos de stock entre el rango de fechas");
                }
                return movimientosStock;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //Obtener la información de resumen de las cantidades movidas agrupadas por año, y dentro de año por tipo de movimiento.
        //Es la cantidad de movidos no el stock actual
        public IEnumerable<dynamic> GetMovimientosCantidadesByTipoAndAno()
        {
            try
            {
                //agrupa las recetas por tiempo de elaboración
                //.GroupBy(r => r.ElaboracionReceta.TiempoElaboracion)

                var movimientosStock = _db.MovimientosStock
                    .Include(m => m.TipoMovimiento)
                    .AsEnumerable() // Cambia a evaluación en memoria
                    .GroupBy(m => new { Year = m.FechaMovimiento.Year, TipoMovimiento = m.TipoMovimiento })
                    .Select(g => new
                    {
                        Ano = g.Key.Year,
                        TipoMovimiento = g.Key.TipoMovimiento.Nombre,
                        CantidadTotal = g.Sum(m => m.Cantidad)
                    })
                    .ToList();

                if (movimientosStock == null || movimientosStock.Count() == 0)
                {
                    throw new MovimientoStockException("No existen movimientos de stock para ese articulo con ese tipo de movimiento");
                }
                return movimientosStock;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

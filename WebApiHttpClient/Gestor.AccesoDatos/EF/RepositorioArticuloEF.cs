using Gestor.LogicaNegocio.Articulos;
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
    public class RepositorioArticuloEF : IRepositorioArticulo
    {
        private GestorContext _db;

        public RepositorioArticuloEF(GestorContext db)
        {
            _db = db;
        }

        public void Add(Articulo articulo)
        {
            try
            {
                if (articulo == null) throw new ArticuloException("El articulo no puede ser nulo");
                _db.Articulos.Add(articulo);
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    SqlException exSql = ex.InnerException as SqlException;
                    if (exSql.Number == 2601)
                        throw new Exception("Ya existe un articulo con ese nombre o codigo");
                }
                throw;
            }
            catch (Exception ex)
            {
                throw new ArticuloException($"El articulo no se pudo agregar. Más info: {ex.Message}");
            }
        }

        public IEnumerable<Articulo> GetAll()
        {
            try
            {
                return _db.Articulos.ToList();
            }
            catch (Exception ex)
            {
                throw new ArticuloException($"No se pudo devolver la lista de articulos. Más info: {ex.Message}");
            }
        }

        public Articulo GetById(int id)
        {
            try
            {
                var articulo = _db.Articulos.Find(id);
                if (articulo == null)
                {
                    throw new ArticuloException("El articulo con ese id no existe");
                }
                return articulo;
            }
            catch (Exception e)
            {
                throw new ArticuloException($"No se ha podido encontrar el articulo con id {id}. Mas info: {e.Message}");
            }
        }

        public IEnumerable<Articulo> GetArticulosOrdenadosAlfabeticamente()
        {
            try
            {
                var articulos = _db.Articulos.OrderBy(a => a.Nombre).ToList();
                if (articulos == null || articulos.Count == 0)
                {
                    throw new Exception("No hay articulos");
                }
                return articulos;
            }
            catch (Exception ex)
            {
                throw new ArticuloException($"Error: {ex.Message}");
            }
        }

        public void Remove(int id)
        {
            try
            {
                Articulo articulo = _db.Articulos.Find(id);
                if (articulo == null)
                {
                    throw new ArticuloException($"No existe un articulo con id:{id}.");
                }
                _db.Articulos.Remove(articulo);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new ArticuloException($"Error. Mas info: {e.Message}");
            }
        }

        public void Remove(Articulo articulo)
        {
            try
            {
                if (articulo == null)
                {
                    throw new ArgumentNullException("El articulo no puede ser nulo");
                }
                _db.Articulos.Remove(articulo);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ArticuloException($"No eliminar el articulo {articulo.Nombre} la lista de articulos. Más info: {ex.Message}");
            }
        }

        public void Update(int id, Articulo articuloModificado)
        {
            try
            {
                var articulo = _db.Articulos.Find(id);
                if (articulo == null)
                {
                    throw new ArticuloException($"No existe el autor con el id {id}");
                }
                articulo.Modificar(articuloModificado);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ArticuloException($"No se ha podido modificar el articulo con id: {id}. Mas info: {ex.Message}");
            }
        }
    }
}

using Gestor.LogicaNegocio.Entidades;
using Gestor.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.AccesoDatos.EF
{
    public class RepositorioParametroEF : IRepositorioParametro
    {
        private GestorContext _db;

        public RepositorioParametroEF(GestorContext db)
        {
            _db = db;
        }
        public Parametro GetByName(string nombre)
        {
            try
            {
                Parametro parametro = _db.Parametros
                .SingleOrDefault(parametro => parametro.Nombre.Equals(nombre));
                return parametro;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int GetValorInt(string nombre)
        {
            try
            {
                Parametro param = GetByName(nombre);
                if (param == null)
                    throw new Exception("No hay un parámetro con ese valor");
                int num = 0;
                var ok = false;
                ok = int.TryParse(param.Valor, out num);
                return num;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public string GetValorString(string nombre)
        {
            try
            {
                Parametro param = GetByName(nombre);
                if (param == null)
                    throw new Exception("No hay un parámetro con ese valor");

                return param.Valor;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public DateTime GetValorDateTime(string nombre)
        {
            try
            {
                Parametro param = GetByName(nombre);
                if (param == null)
                    throw new Exception("No hay un parámetro con ese valor");


                return DateTime.Parse(param.Valor);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public decimal GetValorDecimal(string nombre)
        {
            try
            {
                Parametro param = GetByName(nombre);
                if (param == null)
                    throw new Exception("No hay un parámetro con ese valor");


                return Decimal.Parse(param.Valor);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Add(Parametro obj)
        {
            if (obj == null) throw new ArgumentNullException("El parámetro no puede ser nulo");
            try
            {
                _db.Parametros.Add(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Parametro> GetAll()
        {
            try
            {
                return _db.Parametros.ToList();
            }
            catch (Exception ex)
            {

                throw;
            };
        }

        public Parametro GetById(int id)
        {
            try
            {
                return _db.Parametros.Find(id);
            }
            catch (Exception ex)
            {

                throw;
            };
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Parametro obj)
        {
            if (obj == null) throw new ArgumentNullException("El parámetro no puede ser nulo");
            try
            {
                _db.Parametros.Remove(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Update(String nombreParametro, string valor)
        {
            try
            {
                Parametro param = _db.Parametros.Find(nombreParametro);
                if (param == null)
                    throw new NullReferenceException($"No existe un parámetro {nombreParametro}");
                param.Valor = valor;
                _db.Parametros.Update(param);
                _db.SaveChanges();

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

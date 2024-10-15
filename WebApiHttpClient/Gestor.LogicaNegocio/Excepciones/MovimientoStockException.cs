using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaNegocio.Excepciones
{
    public class MovimientoStockException : Exception
    {
        public MovimientoStockException()
        {
        }

        public MovimientoStockException(string? message) : base(message)
        {
        }

        public MovimientoStockException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected MovimientoStockException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

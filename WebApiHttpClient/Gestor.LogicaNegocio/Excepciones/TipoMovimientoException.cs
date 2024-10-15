using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaNegocio.Excepciones
{
    public class TipoMovimientoException : Exception
    {
        public TipoMovimientoException()
        {
        }

        public TipoMovimientoException(string? message) : base(message)
        {
        }

        public TipoMovimientoException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TipoMovimientoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.LogicaNegocio.Excepciones
{
    public class ArticuloException : Exception
    {
        public ArticuloException()
        {
        }

        public ArticuloException(string? message) : base(message)
        {
        }

        public ArticuloException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ArticuloException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

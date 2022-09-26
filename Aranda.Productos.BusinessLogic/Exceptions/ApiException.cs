using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Aranda.Productos.BusinessLogic.Exceptions
{
    public class ApiException : BaseLogicException
    {
        public ApiException(string message) : base(message, HttpStatusCode.OK, null) { }
        public ApiException(string message, object customData) : base(message, HttpStatusCode.OK, customData) { }

    }
}

using System;
using System.Net;

namespace Aranda.Productos.BusinessLogic.Exceptions
{
    public class BaseLogicException : Exception
    {
        public readonly HttpStatusCode Code;
        public readonly object CustomData;
        public BaseLogicException(string message, HttpStatusCode code, object customData) : base(message)
        {
            this.Code = code;
            this.CustomData = customData;
        }
    }
}

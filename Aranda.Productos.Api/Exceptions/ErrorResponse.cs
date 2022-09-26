using Newtonsoft.Json;
using System;

namespace Aranda.Productos.Api.Exceptions
{
    public class ErrorResponse
    {
        public Int32 Code { get; set; }
        public string Message { get; set; }
        public object CustomData { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}

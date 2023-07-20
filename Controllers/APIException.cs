using System;
using System.Runtime.Serialization;

namespace Genesis.Integracoes.Integracao_DB.Controllers
{
    public class APIException: Exception
    {
        public APIException() : base(){}
        
        public APIException(string msg) : base(msg){}
        
        public APIException(SerializationInfo info, StreamingContext ctx) : base(info, ctx){}
        
        public APIException(string msg, Exception e) : base(msg, e){}
    }
}

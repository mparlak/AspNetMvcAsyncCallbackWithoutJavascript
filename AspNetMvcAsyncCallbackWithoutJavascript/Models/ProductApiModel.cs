using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace AspNetMvcAsyncCallbackWithoutJavascript.Models
{
    public class ProductApiModel
    {
        public long PostId { get; set; }
        public string Html { get; set; }
    }

    public abstract class ApiResponse
    {
        public bool StatusIsSuccessful { get; set; }
        public HttpStatusCode ResponseCode { get; set; }
        public string ResponseResult { get; set; }
    }

    public abstract class ApiResponse<T> : ApiResponse
    {
        public T Data { get; set; }
    }

    public class ProductResponse : ApiResponse<ProductApiModel>
    {

    }
}
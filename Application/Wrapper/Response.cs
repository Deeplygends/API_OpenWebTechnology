using System;
using System.Collections.Generic;
using System.Text;
using Domain.Enums;

namespace Application.Wrapper
{
    public class Response<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public string Uri { get; set; }
        public HttpResponseTypeEnum HttpResponse { get; set; }
        public List<string> Errors { get; set; }
        public Response() { Errors = new List<string>(); }

        public Response(T data, string message = null, HttpResponseTypeEnum httpType = HttpResponseTypeEnum.Ok, string uri = "") : this()
        {
            HttpResponse = httpType;
            Succeeded = true;
            Message = message;
            Data = data;
            Uri = uri;
        }

        public Response(string message) : this()
        {
            Succeeded = false;
            Message = message;
        }
    }
}

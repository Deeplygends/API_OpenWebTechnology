﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Wrapper
{
    public class Response<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public List<string> Errors { get; set; }
        public Response() { Errors = new List<string>(); }

        public Response(T data, string message = null) : this()
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public Response(string message) : this()
        {
            Succeeded = false;
            Message = message;
        }
    }
}

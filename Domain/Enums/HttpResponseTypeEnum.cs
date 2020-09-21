using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    public enum HttpResponseTypeEnum
    {
        Ok = 200,
        Created = 201,
        BadRequest = 400,
        Unauthorized = 401,
        Conflict = 409,
        NoContent = 204,
        NotFound = 404
    }
}

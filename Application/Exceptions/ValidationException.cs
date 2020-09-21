using System;
using System.Collections.Generic;
using System.Text;
using Domain.Enums;
using FluentValidation.Results;

namespace Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("One or more validation failures have occurred.")
        {
            Errors = new List<string>();
        }
        public List<string> Errors { get; }
        public HttpResponseTypeEnum Type { get; set; }
        public ValidationException(IEnumerable<ValidationFailure> failures, HttpResponseTypeEnum type = HttpResponseTypeEnum.BadRequest)
            : this()
        {
            Type = type;
            foreach (var failure in failures)
            {
                Errors.Add(failure.ErrorMessage);
            }
        }
    }
}

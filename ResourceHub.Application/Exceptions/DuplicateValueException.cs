using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceHub.Application.Exceptions
{
    public class DuplicateValueException : Exception
    {
        public DuplicateValueException(string message):base(message)
        {
        }
    }
}

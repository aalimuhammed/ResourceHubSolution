using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceHub.Application.Exceptions
{
    public class VaildateException:Exception
    {
        public VaildateException(string message):base(message)
        {
        }
    }
}

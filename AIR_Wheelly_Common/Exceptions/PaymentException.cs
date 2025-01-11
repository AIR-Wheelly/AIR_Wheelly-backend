using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIR_Wheelly_Common.Exceptions
{
    public class PaymentException : SystemException
    {
        public PaymentException(string? message) : base(message)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIR_Wheelly_Common.DTO
{
    public class CreatePaymentDTO
    {
        public string PaymentMethodNonce {  get; set; }
        public string DeviceData { get; set; } = "";
        public decimal Amount { get; set; }
        public Guid ReservationId { get; set; }
    }
}

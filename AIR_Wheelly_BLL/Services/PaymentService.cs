using Braintree;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIR_Wheelly_BLL.Services
{
    public class PaymentService
    {
        private readonly BraintreeGateway _gateway;

        public PaymentService(IConfiguration config)
        {
            var braintreeSettings = config.GetSection("Braintree");

            _gateway = new BraintreeGateway()
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = braintreeSettings.GetValue<string>("MerchantID"),
                PublicKey = braintreeSettings.GetValue<string>("PublicKey"),
                PrivateKey = braintreeSettings.GetValue<string>("PrivateKey")
            };
        }

        public async Task<string> GenerateClientToken()
        {
            var clientToken = await _gateway.ClientToken.GenerateAsync();

            return clientToken;
        }
    }
}

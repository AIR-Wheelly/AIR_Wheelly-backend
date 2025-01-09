using AIR_Wheelly_Common.DTO;
using AIR_Wheelly_Common.Exceptions;
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

        public async Task CreateTransaction(CreatePaymentDTO dto)
        {
            var request = new TransactionRequest
            {
                Amount = dto.Amount,
                PaymentMethodNonce = dto.PaymentMethodNonce,
                DeviceData = dto.DeviceData,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            Result<Transaction> result = await _gateway.Transaction.SaleAsync(request);

            if (!result.IsSuccess())
            {
                throw new PaymentException(result.Message);
            }

            //TODO: Update reservation payment status (paid = true)
            //dto.ReservationId
        }
    }
}

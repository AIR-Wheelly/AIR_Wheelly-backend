using AIR_Wheelly_Common.DTO;
using AIR_Wheelly_Common.Exceptions;
using Braintree;
using Microsoft.Extensions.Configuration;
using AIR_Wheelly_Common.Interfaces;

namespace AIR_Wheelly_BLL.Services
{
    public class PaymentService
    {
        private readonly BraintreeGateway _gateway;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IConfiguration config, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            var reservation = await _unitOfWork.CarReservationRepository.GetByIdAsync(dto.ReservationId);
            if (reservation == null)
            {
                throw new PaymentException("Reservation not found");
            }
            var request = new TransactionRequest
            {
                Amount = (decimal)reservation.TotalPrice,
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

          
            reservation.IsPaid = true;
             _unitOfWork.CarReservationRepository.Update(reservation);
             await _unitOfWork.CompleteAsync();
        }
    }
}

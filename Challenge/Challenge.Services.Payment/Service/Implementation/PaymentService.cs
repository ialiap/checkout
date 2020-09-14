using System;
using System.Threading.Tasks;
using AutoMapper;
using Challenge.Services.Payment.Common.Behavior;
using Challenge.Services.Payment.Model.Request;
using Challenge.Services.Payment.Model.Response;
using Challenge.Services.Payment.Repository;
using Challenge.Services.Payment.Service.Protocol;

namespace Challenge.Services.Payment.Service.Implementation
{
    public class PaymentService : IPaymentService
    {
        private readonly PaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly IBankService _bankService;

        public PaymentService(PaymentRepository paymentRepository, IMapper mapper, IBankService bankService)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _bankService = bankService;
        }

        [BindingModelValidation]
        public async Task<GetCreatePaymentResponseModel> ProcessPayment(CreatePaymentBindingModel requestModel)
        {
            var payment = _mapper.Map<Domain.Payment>(requestModel);
            var savedPayment = await _paymentRepository.AddAsync(payment);
            var processedPaymentResult = await _bankService.ProcessPaymentAsync(savedPayment);

            savedPayment.TrackingId = processedPaymentResult.TrackingId;
            savedPayment.ModificationDateTime = DateTime.Now;
            savedPayment.Status = payment.Status;

            await _paymentRepository.UpdateAsync(savedPayment);
            return _mapper.Map<GetCreatePaymentResponseModel>(savedPayment);

        }

        public async Task<GetPaymentResponseModel> GetByTrackingId(string trackingId)
        {
            return _mapper.Map<GetPaymentResponseModel>(await _paymentRepository.FindAsync(x=>x.TrackingId== trackingId));
        }

    }
}
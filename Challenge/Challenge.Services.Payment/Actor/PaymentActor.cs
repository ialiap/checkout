//using System;
//using Akka.Actor;
//using AutoMapper;
//using Challenge.Services.Payment.Command;
//using Challenge.Services.Payment.Model.Response;
//using Challenge.Services.Payment.Repository;
//using Challenge.Services.Payment.Service.Protocol;

//namespace Challenge.Services.Payment.Actor
//{

//    public class PaymentActor : ReceiveActor
//    {
//        private readonly PaymentRepository _paymentRepository;
//        private readonly IBankService _bankService;
//        private readonly IMapper _mapper;

//        public PaymentActor(PaymentRepository materialRepository, IMapper mapper, IBankService bankService)
//        {
//            _paymentRepository = materialRepository;
//            _mapper = mapper;
//            _bankService = bankService;

//            ReceiveAsync<CreatePaymentCommand>(async command =>
//            {
//                var payment = _mapper.Map<Domain.Payment>(command);
//                var savedPayment = await _paymentRepository.AddAsync(payment);
//                var processedPaymentResult = await _bankService.ProcessPaymentAsync(savedPayment);

//                savedPayment.TrackingId = processedPaymentResult.TrackingId;
//                savedPayment.ModificationDateTime = DateTime.Now;
//                savedPayment.Status = payment.Status;

//                await _paymentRepository.UpdateAsync(savedPayment);
//                Sender.Tell(_mapper.Map<GetCreatePaymentResponseModel>(savedPayment));
//            });

//            ReceiveAsync<GetPaymentByTrackingIdCommand>(async command =>
//            {
//                var payment = await _paymentRepository.FindAsync(x => x.TrackingId == command.TrackingId);
//                Sender.Tell(_mapper.Map<GetPaymentResponseModel>(payment));
//            });

//        }


//    }
//}

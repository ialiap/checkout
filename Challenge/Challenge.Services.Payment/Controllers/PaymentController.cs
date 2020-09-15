using System.Net;
using System.Threading.Tasks;
using Challenge.Services.Payment.Model.Request;
using Challenge.Services.Payment.Model.Response;
using Challenge.Services.Payment.Service.Protocol;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Challenge.Services.Payment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(ILogger<PaymentController> logger, IPaymentService paymentService)
        {
            _logger = logger;
            _paymentService = paymentService;
        }

        /// <summary>
        /// Returns a payment detail using tracking unique identifier
        /// </summary>
        /// <param name="trackingId">The payment tracking unique identifier </param>
        /// <returns> Payment Response Model</returns>
        [Route("{trackingId}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetPaymentResponseModel))]
        public async Task<IActionResult> Get([FromRoute] string trackingId)
        {
            return Ok(await _paymentService.GetByTrackingId(trackingId));
        }

        /// <summary>
        /// Process a new payment
        /// </summary>
        /// <param name="requestModel">New Payment Detail </param>
        /// <returns>Create Payment Response </returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(CreatePaymentResponseModel))]
        public async Task<IActionResult> Post([FromBody] CreatePaymentBindingModel requestModel)
        {
            return Ok(await _paymentService.ProcessPayment(requestModel));
        }


    }
}

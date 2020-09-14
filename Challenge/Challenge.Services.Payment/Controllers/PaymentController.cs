using System.Net;
using System.Threading.Tasks;
using Akka.Actor;
using Challenge.Services.Payment.Actor;
using Challenge.Services.Payment.Command;
using Challenge.Services.Payment.Model.Request;
using Challenge.Services.Payment.Model.Response;
using Challenge.Services.Payment.Service;
using Challenge.Services.Payment.Service.Implementation;
using Challenge.Services.Payment.Service.Protocol;
using LUM.Services.Material.Command;
using LUM.Services.Material.Model.Query;
using LUM.Services.Material.Model.Request;
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
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetCreatePaymentResponseModel))]
        public async Task<IActionResult> Post([FromBody] CreatePaymentBindingModel requestModel)
        {
            return Ok(await _paymentService.ProcessPayment(requestModel));
        }


    }
}

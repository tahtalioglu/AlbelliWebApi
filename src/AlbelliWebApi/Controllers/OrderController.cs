using AlbelliWebApi.Services;
using AlbelliWebApi.Models.Responses;
using AlbelliWebApi.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace AlbelliWebApi.Controllers
{
    [Route("v1/orders")]
    [Produces("application/json")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(BaseResponse<string>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "No value found for requested filter.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Request not accepted.")]
        [SwaggerResponse((int)HttpStatusCode.Forbidden, Description = "Access not allowed.")]
        [HttpPost]
        public IActionResult Create([FromBody]OrderRequest request)
        {
            var response = _orderService.Create(request);

            if (response == null)
            {
                return NotFound(response);
            }
            if (!response.HasError && response?.Result == null)
            {
                return NotFound(response);
            }

            if (!response.HasError && response?.Result != null)
            {
                return Ok(response);
            }         

            return BadRequest(response);
        }
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(BaseResponse<OrderResponse>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "No value found for requested filter.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Request not accepted.")]
        [SwaggerResponse((int)HttpStatusCode.Forbidden, Description = "Access not allowed.")]
        [HttpGet]
        public IActionResult  Get(string orderId)
        {
            var response = _orderService.Get(orderId);

            if (!response.HasError && response.Result != null)
            {
                return Ok(response);
            }

            if (!response.HasError && response.Result == null)
            {
                return NotFound(response);
            }

            return BadRequest(response);
            

        }
    }
}
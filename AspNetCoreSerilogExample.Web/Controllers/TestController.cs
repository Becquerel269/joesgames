using AspNetCoreSerilogExample.Web.Data.Models;
using AspNetCoreSerilogExample.Web.Services.Processing;
using AspNetCoreSerilogExample.Web.Services.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace AspNetCoreSerilogExample.Web.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        private readonly IProcessOrderService _processOrderService;

        public TestController(ILogger<TestController> logger, IValidateOrderService validateOrderService, IProcessOrderService processOrderService)
        {
            _logger = logger;

            _processOrderService = processOrderService;
        }

        [HttpPost]
        [Route("api/orders")]
        public ActionResult<IOrder> Add([FromBody] OrderDTO orderdto)
        {
            _logger.LogInformation($"Input text: {orderdto.Name}");

            var returnedOrder = _processOrderService.SubmitOrder(orderdto);
            if (returnedOrder == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(returnedOrder);
        }

        [HttpPut]
        [Route("api/orders")]
        public ActionResult<IOrder> Update([FromBody] OrderDTO orderdto)
        {
            _logger.LogInformation($"Input text: {orderdto.Name}");

            var updatedOrder = _processOrderService.SubmitOrder(orderdto);
            if (updatedOrder == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(updatedOrder);
        }

        //https://localhost:5001/api/test/order
        [HttpGet]
        [Route("api/orders/{id}")]
        [Produces("application/json")]
        public ActionResult<IOrder> GetById(string id)
        {
            _logger.LogInformation(
                "Input text: {Input}",
                id);
            return Ok(_processOrderService.GetOrder(id));
        }

        [HttpGet]
        [Route("api/orders")]
        [Produces("application/json")]
        public ActionResult<List<Order>> GetOrders()
        {
            return Ok(_processOrderService.GetOrders());
        }
    }
}
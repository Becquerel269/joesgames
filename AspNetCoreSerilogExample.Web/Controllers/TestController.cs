using AspNetCoreSerilogExample.Web.Data.Models;
using AspNetCoreSerilogExample.Web.Extensions;
using AspNetCoreSerilogExample.Web.Services.Processing;
using AspNetCoreSerilogExample.Web.Services.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<IOrder>> Add([FromBody] OrderDTO orderdto)
        {
            _logger.LogInformation($"Input text: {orderdto.Name}");

            var returnedOrder = await _processOrderService.SubmitOrder(orderdto);
            if (returnedOrder == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(returnedOrder);
        }

        [HttpPut]
        [Route("api/orders")]
        public async Task<ActionResult<IOrder>> Update([FromBody] OrderDTO orderdto)
        {
            _logger.LogInformation($"Input text: {orderdto.Name}");

            var status = await _processOrderService.UpdateOrder(orderdto);

            return StatusCode(status.ToHTTPStatusCode());
        }

        //https://localhost:5001/api/test/order
        [HttpGet]
        [Route("api/orders/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<IOrder>> GetById(string id)
        {
            _logger.LogInformation(
                "Input text: {Input}",
                id);
            return Ok(await _processOrderService.GetOrder(id));
        }

        [HttpGet]
        [Route("api/orders")]
        [Produces("application/json")]
        public async Task<ActionResult<List<Order>>> GetOrders()
        {
            return Ok(await _processOrderService.GetOrders());
        }

        [HttpDelete]
        [Route("api/orders/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<List<Order>>> Delete(string id)
        {
            return Ok(await _processOrderService.DeleteOrder(id));
        }
    }
}
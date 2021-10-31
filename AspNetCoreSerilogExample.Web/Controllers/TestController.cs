using AspNetCoreSerilogExample.Web.Data.Models;
using AspNetCoreSerilogExample.Web.Services.Processing;
using AspNetCoreSerilogExample.Web.Services.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
        public ActionResult<IOrder> Add([FromBody]Order order)
        {
            _logger.LogInformation($"Input text: {order.Name}");

            var returnedOrder = _processOrderService.SubmitOrder(order);
            if (returnedOrder == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(returnedOrder);
        }
        //do the same for the put as we have done for the post
        //try move the logic from OrderData to ProcessOrder/fileprocessing
        //then add unit tests including a new class for FileProcessingTests
        //look at Get method make sure it is working
        [HttpPut]
        [Route("api/orders")]
        public IOrder Update(string input)
        {
            _logger.LogInformation($"Input text: {input}");
            string[] items = { "item1", "item2" };
            var dummyorder = new Order(

                name: "order1",
                id: "1",
                items: items
            );
            return _processOrderService.SubmitOrder(dummyorder);
        }

        //https://localhost:5001/api/test/order
        [HttpGet]
        [Route("api/orders")]
        [Produces("application/json")]
        public IOrder Order(string id)
        {
            id = "2";
            _logger.LogInformation(
                "Input text: {Input}",
                id);
            return _processOrderService.GetOrder(id);
        }

        private void MoreWork()
        {
            _logger.LogInformation("More log context");
        }
    }
}
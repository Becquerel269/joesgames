using AspNetCoreSerilogExample.Web.Data.Models;
using AspNetCoreSerilogExample.Web.Services.Processing;
using AspNetCoreSerilogExample.Web.Services.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AspNetCoreSerilogExample.Web.Controllers
{
    //[Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        
        private readonly IProcessOrder _processOrder;

        public TestController(ILogger<TestController> logger, IValidateOrder validateOrder, IProcessOrder processOrder)
        {
            _logger = logger;
            
            _processOrder = processOrder;
        }

        [HttpPost]
        [Route("api/orders")]
        public IOrder Add([FromBody]Order order)
        {
            _logger.LogInformation($"Input text: {order.Name}");
           
            
            return _processOrder.SubmitOrder(order);
        }

        [HttpPut]
        [Route("api/orders")]
        public IOrder Update(string input)
        {
            _logger.LogInformation($"Input text: {input}");
            string[] items = { "item1", "item2" };
            IOrder dummyorder = new Order(

                name: "order1",
                id: "1",
                items: items
            );
            return _processOrder.SubmitOrder(dummyorder);
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
            return _processOrder.GetOrder(id);
        }

        private void MoreWork()
        {
            _logger.LogInformation("More log context");
        }
    }
}
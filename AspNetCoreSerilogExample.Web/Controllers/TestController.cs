using AspNetCoreSerilogExample.Web.DataLayer.Models;
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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly IProcessOrder _processOrder;

        public TestController(ILogger<TestController> logger, IProcessOrder validateOrder)
        {
            _logger = logger;
            _processOrder = validateOrder;
        }

        [HttpGet]
        public string Submit(string input)
        {
            _logger.LogInformation($"Input text: {input}");
            Order order = new Order(Id: "id", Name: "name");
            return _processOrder.ProcessOrder("valid order").ToString();
        }

        [HttpGet]
        public void StructuredLog(string input)
        {
            _logger.LogInformation(
                "Input text: {Input}",
                input);
        }

        [HttpGet]
        public void EnrichLog(string input, string inputType)
        {
            _logger.LogInformation("Input text: {Input} with {InputType}", input, inputType);
        }

        [HttpGet]
        public void ScopeLog(string input, string additionalContext)
        {
            using (_logger.BeginScope(new Dictionary<string, object> { { "AdditionalContext", additionalContext } }))
            {
                _logger.LogInformation("Input text with scope: {Input}", input);

                MoreWork();
            }
        }

        [HttpGet]
        public async Task TimeLog()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            await Task.Delay(new Random().Next(2000));
            stopwatch.Stop();
            _logger.LogInformation("Long task: {LongTimeElapsedMs}ms", stopwatch.ElapsedMilliseconds);
        }

        private void MoreWork()
        {
            _logger.LogInformation("More log context");
        }
    }
}
﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AspNetCoreMsLoggerExample.Web.Services.Validation;

namespace AspNetCoreSerilogExample.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly IValidateOrder _validateOrder;

        public TestController(ILogger<TestController> logger, IValidateOrder validateOrder)
        {
            _logger = logger;
            _validateOrder = validateOrder;
        }

        [HttpGet]
        public void FlatLog(string input)
        {
            _logger.LogInformation($"Input text: {input}");
            var test = _validateOrder.IsOrderValid("test");
            _logger.LogInformation($"Validated order: {test}");

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

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDBTest.Services;
using Rescuer.Mongo.Core.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MongoDBTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ITestServices _testServices;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            ITestServices testServices)
        {
            _logger = logger;
            _testServices = testServices;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var asd = _testServices.GetAll();
            _logger.Log(LogLevel.Information, "OK", "", "", "");
            var rng = new Random();
            var a = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });

            return asd;
            // _genericRepository.Insert(a);
            // 
            // var tek = _genericRepository.GetById("5f7c6e59e131d675b85d4a13");
            // var cift = _genericRepository.Find(f => f.ID == "5f7c6e59e131d675b85d4a13");
            // cift = _genericRepository.Table.Where(w => w.ID == "5f7c6e59e131d675b85d4a13").ToList();
            //return cift;
        }
    }
}
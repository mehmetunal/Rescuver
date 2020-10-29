using System.Collections.Generic;

namespace MongoDBTest.Services
{
    public interface ITestServices 
    {
        IEnumerable<WeatherForecast> GetAll();
    }
}
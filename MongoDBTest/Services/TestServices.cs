using Rescuer.Mongo.Core.Repository.Interface;
using System.Collections.Generic;
using Rescuer.Core.IoC;

namespace MongoDBTest.Services
{
    public class TestServices : IBaseRegisterType, ITestServices
    {
        private readonly IMongoRepository<WeatherForecast> _genericRepository;
        public TestServices(IMongoRepository<WeatherForecast> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public IEnumerable<WeatherForecast> GetAll()
            => _genericRepository.Get();
    }
}
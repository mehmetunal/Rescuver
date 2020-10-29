using Rescuer.Entites;
using Rescuer.Entites.Mongo;
using System;
using Rescuer.Core.Entities;

namespace MongoDBTest
{
    public class WeatherForecast : BaseEntity, IEntity
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }


    }
}

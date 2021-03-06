﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Rescuer.Framework.Extensions
{
    public static class MongoDbExtensions
    {
        public static IServiceCollection AddMongoDbConfig(this IServiceCollection services,
            IConfiguration configuration)
        {
            var mongoDbConnection = configuration.GetSection("MongoConnection:ConnectionString").Value;
            if (string.IsNullOrEmpty(mongoDbConnection)) return services;

            var client = new MongoClient(mongoDbConnection);

            var databaseName = new MongoUrl(mongoDbConnection)?.DatabaseName;
            if (string.IsNullOrEmpty(databaseName))
            {
                databaseName = configuration.GetSection("MongoConnection:Database").Value;
                if (client != null && client.Settings != null)
                {
                    var setting = client.Settings.Clone();
                    setting.Credential = MongoCredential.CreateCredential(databaseName, client.Settings.Credential.Username, client.Settings.Credential.Password);
                    client = new MongoClient(setting);
                }
            }

            services.AddScoped(typeof(IMongoDatabase), c => client.GetDatabase(databaseName));

            return services;
        }
    }
}
using AccountTransaction.MessageBus.RabbitMQSender;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountTransaction.MessageBus
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(svc =>
                new RabbitMQMessageConfiguration
                (
                    configuration.GetSection("MessageQueueConnection:host").Value,
                    configuration.GetSection("MessageQueueConnection:username").Value,
                    configuration.GetSection("MessageQueueConnection:password").Value
                ));

            services.AddSingleton<IRabbitMQMessageSender, RabbitMQMessageSender>();
            return services;
        }
    }

}

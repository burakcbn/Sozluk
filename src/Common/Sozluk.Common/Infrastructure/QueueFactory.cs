﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sozluk.Common.Infrastructure
{
    public static class QueueFactory
    {
        public static void SendMessage(string exchangeName, string exchangeType, string queueName, object obj)
        {
            var channel = CreateBasicConsumer()
                                .EnsureExchange(exchangeName, exchangeType)
                                .EnsureQueue(queueName, exchangeName)
                                .Model;

            var body=Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj));
            channel.BasicPublish(exchangeName, queueName, null, body);
        }

        public static EventingBasicConsumer CreateBasicConsumer()
        {
            var factory = new ConnectionFactory() { HostName = SozlukConstants.RabbitMQHost };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            return new(channel);
        }

        public static EventingBasicConsumer EnsureExchange(this EventingBasicConsumer consumer,
                                                           string exchangeName,
                                                           string exchangeType = SozlukConstants.DefaultExchangeType)
        {

            consumer.Model.ExchangeDeclare(exchangeName, exchangeType, false, false);
            return consumer;
        }
        public static EventingBasicConsumer EnsureQueue(this EventingBasicConsumer consumer,
                                                          string queueName,
                                                          string exchangeName = SozlukConstants.DefaultExchangeType)
        {

            consumer.Model.QueueDeclare(queueName, false, false, false, null);
            consumer.Model.QueueBind(queueName, exchangeName, queueName);
            return consumer;
        }
    }
}
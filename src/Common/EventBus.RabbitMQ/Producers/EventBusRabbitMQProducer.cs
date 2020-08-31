using EventBus.RabbitMQ.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EventBus.RabbitMQ.Producers
{
    public class EventBusRabbitMQProducer
    {
        private readonly IRabbitMQConnection rabbitMQConnection;

        public EventBusRabbitMQProducer(IRabbitMQConnection rabbitMQConnection)
        {
            this.rabbitMQConnection = rabbitMQConnection ?? throw new ArgumentNullException(nameof(rabbitMQConnection));
        }

        public void PublishBasketCheckout(string queueName, BasketCheckoutEvent publishModel) 
        {
            using (var channel = rabbitMQConnection.CreateModel()) 
            {
                channel.QueueDeclare(queue: queueName,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);
                var message = JsonConvert.SerializeObject(publishModel);
                var body = Encoding.UTF8.GetBytes(message);

                IBasicProperties basicProperties = channel.CreateBasicProperties();
                basicProperties.Persistent = true;
                basicProperties.DeliveryMode = 2;

                channel.ConfirmSelect();
                channel.BasicPublish(
                    exchange:"",
                    routingKey:queueName,
                    mandatory:false,
                    basicProperties: basicProperties,
                    body: body
                    );
                channel.WaitForConfirmsOrDie();

                channel.BasicAcks += (sender, eventArgs) => {

                    Console.WriteLine("Sent Message RabbitMQ");
                };
                channel.ConfirmSelect();
            }
        }
    }
}

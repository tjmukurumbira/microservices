
using AutoMapper;
using EventBus.RabbitMQ.Common;
using MediatR;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Newtonsoft.Json;
using Ordering.Application.Commands;
using Ordering.Core.Entities.Repositories;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using RabbitMQ.Client;
using EventBus.RabbitMQ;

namespace Ordering.API.RabbitMQ
{
    public class EventBusRabbitMQConsumer
    {
        private readonly IRabbitMQConnection rabbitMQConnection;
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly IOrderRepository orderRepository;

        public EventBusRabbitMQConsumer(IRabbitMQConnection rabbitMQConnection,
            IMediator mediator,
            IMapper mapper,
            IOrderRepository orderRepository)
        {
            this.rabbitMQConnection = rabbitMQConnection ?? throw new ArgumentNullException(nameof(rabbitMQConnection));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public void Consume() 
        {
            var channel = rabbitMQConnection.CreateModel();
            channel.QueueDeclare(queue: EventBusConstants.BasketCheckoutQueue, durable: false, exclusive: false, autoDelete: false,null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += ReceivedEvent;
            channel.BasicConsume( consumer: consumer, queue: EventBusConstants.BasketCheckoutQueue, autoAck:true, consumerTag:"",noLocal: false, exclusive:false);
        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            if (e.RoutingKey == EventBusConstants.BasketCheckoutQueue) 
            {
                var message = Encoding.UTF8.GetString(e.Body.Span);
                var basketCheckoutEvent = JsonConvert.DeserializeObject(message);

                var command = mapper.Map<CheckoutOrderCommand>(basketCheckoutEvent);

                await mediator.Send(command);
            }
        }

        public void Disconnect() 
        {
            
        }
    }
}

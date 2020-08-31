﻿using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EventBus.RabbitMQ
{
    public class RabbitMqConnection : IRabbitMQConnection
    {
        private readonly IConnectionFactory connectionFactory;
        private  IConnection connection;
        private bool disposed;

        public RabbitMqConnection(
            IConnectionFactory connectionFactory
           )
        {
            this.connectionFactory = connectionFactory;

            if (!IsConnected) 
            {
                TryConnect();
            }
             
        }
        public bool IsConnected { get { return connection != null && connection.IsOpen && !disposed; } }

        public IModel CreateModel()
        {
            if (!IsConnected)
                throw new InvalidOperationException("No rabbit connection");

            return connection.CreateModel();
        }

        public void Dispose()
        {
            if (disposed)
                return;

            try
            {
                connection.Dispose();
                disposed = true;
            }
            catch {
                throw;
            }
        }

        public bool TryConnect()
        {
            try {
                connection = connectionFactory.CreateConnection();
            }
            catch (BrokerUnreachableException ex) 
            {
                Thread.Sleep(30000);
                connection = connectionFactory.CreateConnection();

            }
                         
            return IsConnected;
        }
    }
}

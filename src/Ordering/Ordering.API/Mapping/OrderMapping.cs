﻿using AutoMapper;
using EventBus.RabbitMQ.Events;
using Ordering.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Mapping
{
    public class OrderMapping :Profile
    {
        public OrderMapping()
        {
            CreateMap<CheckoutOrderCommand, BasketCheckoutEvent>().ReverseMap();
        }
    }
}

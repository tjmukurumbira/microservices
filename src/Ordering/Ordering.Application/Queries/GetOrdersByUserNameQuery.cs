using MediatR;
using Ordering.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Queries
{
    public class GetOrdersByUserNameQuery : IRequest<IEnumerable<OrderResponse>>
    {
        public string Username { get; set; }

        public GetOrdersByUserNameQuery(string username)
        {
            Username = username;
        }
    }
}

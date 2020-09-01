using FluentValidation.Results;
using MediatR;
using Ordering.Application.Mappers;
using Ordering.Application.Queries;
using Ordering.Application.Responses;
using Ordering.Core.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Handlers
{
    public class GetOrdersByUserNameHandler : IRequestHandler<GetOrdersByUserNameQuery, IEnumerable<OrderResponse>>
    {
        private readonly IOrderRepository orderRepository;

        public GetOrdersByUserNameHandler(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }
        public async Task<IEnumerable<OrderResponse>> Handle(GetOrdersByUserNameQuery request, CancellationToken cancellationToken)
        {
            var orders = await orderRepository.GetOrdersByUsernameAsync(request.Username);

            var ordersResponseList = OrderMapper.Mapper.Map<IEnumerable<OrderResponse>>(orders);

            return ordersResponseList;
        }
    }
}

using AutoMapper;
using AutoMapper.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Mappers
{
    public static class OrderMapper 
    {
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() => {

            var config = new MapperConfiguration(cfg => 
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<OrderMappingProfile>();
            });

            return config.CreateMapper();
        });

        public static IMapper Mapper => lazy.Value;
    }
}

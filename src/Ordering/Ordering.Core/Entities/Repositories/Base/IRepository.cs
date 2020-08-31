﻿using Ordering.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Core.Entities.Repositories.Base
{
    public interface IRepository<T>  where T: Entity
    {
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T,bool>> predicate);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate =null,
            Func<IQueryable<T>,IQueryable<T>> orderby = null,
            string includeString  =null,
            bool disableTracking = true 
            );

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
           Func<IQueryable<T>, IQueryable<T>> orderby = null,
           List<Expression<Func<T,object>>> includes = null,
           bool disableTracking = true
           );

        Task<T> GetByidAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpadateAsync(T entity);

        Task DeleteAsync(T entity);






    }
}

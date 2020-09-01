﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Ordering.Core.Entities.Base;
using Ordering.Core.Entities.Repositories.Base;
using Ordering.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T: Entity
    {
        protected readonly OrderContext orderContext;

        public Repository(OrderContext orderContext)
        {
            this.orderContext = orderContext ?? throw new ArgumentNullException(nameof(orderContext));
        }

        public async Task<T> AddAsync(T entity)
        {
            await orderContext.Set<T>().AddAsync(entity);
            await orderContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            orderContext.Set<T>().Remove(entity);
            await orderContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await orderContext.Set<T>().ToListAsync();

        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await orderContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IQueryable<T>> orderby = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<T> query = orderContext.Set<T>();
            if (disableTracking)
                query = query.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(includeString))
                query = query.Include(includeString);

            if (predicate != null)
                query = query.Where(predicate);

            if (orderby != null)
                return await orderby(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IQueryable<T>> orderby = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = orderContext.Set<T>();
            if (disableTracking)
                query = query.AsNoTracking();

            if (includes != null)
                query = includes.Aggregate(query,(current,include)=> current.Include(include));

            if (predicate != null)
                query = query.Where(predicate);

            if (orderby != null)
                return await orderby(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await orderContext.Set<T>().FindAsync(id);
        }

        public async Task UpadateAsync(T entity)
        {
            orderContext.Entry(entity).State = EntityState.Modified;
            await orderContext.SaveChangesAsync();
        }
    }
}

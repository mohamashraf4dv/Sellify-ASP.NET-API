using Sellify.Application.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sellify.Application.Contracts
{
    public interface IUnitOfWork
    {
        public IGenericRepositoryWithNoDeleteAndUpdate<Order> Order { get; }
        public IGenericRepositoryWithNoDeleteAndUpdate<OrderItem> OrderItem { get; }
        Task<int> SaveChangesAsync();
    }
}

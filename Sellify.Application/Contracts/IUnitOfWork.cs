using Sellify.Application.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sellify.Application.Contracts
{
    public interface IUnitOfWork
    {
        public IGenericRepositoryWithNoSoftDeleteAndUpdate<Order> Order { get; }
        public IGenericRepositoryWithNoSoftDeleteAndUpdate<OrderItem> OrderItem { get; }
        Task<int> SaveChangesAsync();
    }
}

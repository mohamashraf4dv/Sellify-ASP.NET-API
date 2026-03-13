using System;
using System.Collections.Generic;
using System.Text;

namespace Sellify.Application.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}

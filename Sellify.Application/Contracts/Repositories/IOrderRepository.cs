using System.Linq.Expressions;

namespace Sellify.Application.Contracts.Repositories
{
    public interface IOrderRepository:IGenericRepositoryWithNoSoftDeleteAndUpdate<Order>
    {

    }
}

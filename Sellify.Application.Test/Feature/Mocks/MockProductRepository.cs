using Moq;
using Sellify.Application.Contracts.Repositories;

namespace Sellify.Application.Test.Feature.Mocks
{
    public class MockProductRepository
    {
        public static Mock<IProductRepository> GetMockProductRepository()
        {
            var mock = new Mock<IProductRepository>();
            //-- will make setups here --
            return mock;
        }

    }
}

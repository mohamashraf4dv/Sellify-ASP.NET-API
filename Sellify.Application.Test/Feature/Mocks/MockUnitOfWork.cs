using Moq;
using Sellify.Application.Contracts;

namespace Sellify.Application.Test.Feature.Mocks
{
    public class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetMockUnitOfWork()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            return mock;
        }
    }
}

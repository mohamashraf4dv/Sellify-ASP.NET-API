using FluentAssertions;
using Moq;
using Sellify.Application.Contracts.Services;
using Sellify.Application.Features.Authentication.Query.GetUserProfile;
using Sellify.Application.Test.Feature.Mock;
namespace Sellify.Application.Test.Feature.Authentication.Query.GetUserProfile
{
    public class GetUserProfileQueryTests
    {
        private Mock<IUserService> _userServiceMock;
        public GetUserProfileQueryTests() 
        {
            _userServiceMock = MockUserService.GetMockUserService();
        }

        public async Task Handle_WhenValidUserId_ShouldReturnUserProfile()
        {
            //Arrange
            var query = new GetUserProfileQuery("Guid-1");
            var handler = new GetUserProfileQueryHandler(_userServiceMock.Object);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.data.Should().NotBeNull();
            result.statusCode.Should().Be(200);
        }

        public async Task Handle_WhenInvalidUserId_ShouldnotReturnUserProfileAndReturnStatusCode404()
        {
            //Arrange
            var query = new GetUserProfileQuery("GuidInvalid-1");
            var handler = new GetUserProfileQueryHandler(_userServiceMock.Object);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.data.Should().BeNull();
            result.statusCode.Should().Be(404);
        }
    }
}

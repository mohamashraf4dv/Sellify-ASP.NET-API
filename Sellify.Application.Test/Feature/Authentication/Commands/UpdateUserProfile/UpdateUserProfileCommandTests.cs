using FluentAssertions;
using Moq;
using Sellify.Application.Contracts.Services;
using Sellify.Application.Features.Authentication.Commands.UpdateUserProfile;
using Sellify.Application.Features.Token.Commands.UpdateAccessToken;
using Sellify.Application.Test.Feature.Mocks;

namespace Sellify.Application.Test.Feature.Authentication.Commands.UpdateUserProfile
{
    public class UpdateUserProfileCommandTests
    {
        private Mock<IUserService> _userServiceMock;
        public UpdateUserProfileCommandTests()
        {
            _userServiceMock = MockUserService.GetMockUserService();
        }
        [Fact]
        public async Task Handle_WhenValidData_ShouldUpdateAndReturnStatusCode200()
        {
            //Arrange
            var command = new UpdateUserProfileCommand(new UpdateUserProfileDTO("newFirstName", "newLastName", "moash22@gmail", "0123123213", "newUserName"));
            var handler = new UpdateUserProfileCommandHandler(_userServiceMock.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert

            result.statusCode.Should().Be(200);
            
        }

        [Fact]
        public async Task Handle_WhenInvalidData_ShouldnotUpdateAndReturnStatusCode404()
        {
            //Arrange
            var command = new UpdateUserProfileCommand(new UpdateUserProfileDTO("newFirstName", "newLastName", "invalid@gmail", "0123123213", "newUserName"));
            var handler = new UpdateUserProfileCommandHandler(_userServiceMock.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert

            result.statusCode.Should().Be(404);

        }
    }

}

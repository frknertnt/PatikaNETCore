using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.UnitTest.TestSetup;
using Xunit;

namespace WebApi.UnitTest.Application.AuthorOperation.Command.DeleteAuthor
{
    public class DeleteAuthorCommandValidatorTests : IClassFixture<CommonTextFixture>
    {

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidAuthorIdIsGiven_Validator_ShouldBeReturnErrors(int authorId)
        {
            //arrange
            DeleteAuthorCommand command = new DeleteAuthorCommand(null!);
            command.Id = authorId;

            //act
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Theory]
        [InlineData(200)]
        [InlineData(2)]
        public void WhenValidBookIdisGiven_Validator_ShouldNotBeReturnError(int authorId)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(null!);
            command.Id = authorId;

            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);

        }

    }
}

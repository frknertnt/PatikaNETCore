using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DBOperations;
using WebApi.UnitTest.TestSetup;
using Xunit;

namespace WebApi.UnitTest.Application.AuthorOperation.Command.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTest : IClassFixture<CommonTextFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateAuthorCommandValidatorTest(CommonTextFixture testFixture)
        {
            _context = testFixture.context;
        }

        [Theory]
        [InlineData(0, "Lor", "asd")]
        [InlineData(0, "Lo ", "asdf ")]
        [InlineData(0, "Lor", "fdsa")]
        [InlineData(-1, "Lord Of", " ")]
        [InlineData(1, " ", " ")]
        [InlineData(1, "", "ASDF")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int authorId, string firstname, string lastname)
        {
            //arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null!);
            command.Model = new UpdateAuthorModel()
            {
                FirstName = firstname,
                LastName = lastname
            };
            command.Id = authorId;
            //act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [InlineData(1, "Lord Of The Rings", "QWERT")]
        [Theory]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors(int authorId, string firstname, string lastname)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = new UpdateAuthorModel()
            {
                FirstName = firstname,
                LastName = lastname
            };
            command.Id = authorId;

            UpdateAuthorCommandValidator validations = new UpdateAuthorCommandValidator();
            var result = validations.Validate(command);

            result.Errors.Count.Should().Be(0);
        }


    }
}

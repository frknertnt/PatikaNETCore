using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using WebApi.UnitTest.TestSetup;
using Xunit;

namespace WebApi.UnitTest.Application.GenreOperation.Command.UpdateGenre
{
    public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTextFixture>
    {
        private readonly BookStoreDbContext context;
        public UpdateGenreCommandValidatorTests(CommonTextFixture c)
        {
            context = c.context;
        }
        [Theory]
        [InlineData("as")]
        [InlineData("as ")]
        [InlineData(" a ")]
        [InlineData("asd")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string name)
        {
            //arrange
            UpdateGenreCommand command = new UpdateGenreCommand(null!);
            command.Model = new UpdateGenreModel() { Name = name };

            //act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }
        [Theory]
        [InlineData("qwer")]
        [InlineData("rth weq")]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors(string name)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.Model = new UpdateGenreModel() { Name = name };

            UpdateGenreCommandValidator validations = new UpdateGenreCommandValidator();
            var result = validations.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}

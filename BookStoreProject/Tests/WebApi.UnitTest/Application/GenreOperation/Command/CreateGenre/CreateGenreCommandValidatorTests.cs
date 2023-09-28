using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.UnitTest.TestSetup;
using Xunit;

namespace WebApi.UnitTest.Application.GenreOperation.Command.CreateGenre
{
    public class CreateGenreCommandValidatorTests : IClassFixture<CommonTextFixture>
    {
        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData("q")]
        [InlineData("qw")]
        [InlineData("qwe")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string name)
        {
            // Arrange 
            CreateGenreCommand command = new CreateGenreCommand(null!, null!);
            command.Model = new CreateGenreModel { Name = name };

            // Act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            // Assert

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("qwer")]
        [InlineData("qwert")]
        [InlineData("trewq")]
        [InlineData("qwe a")]
        [InlineData(" q   ")]
        public void WhenValidInputAreGiven_Validator_ShouldBeReturnErrors(string name)
        {
            CreateGenreCommand command = new CreateGenreCommand(null!, null!);
            command.Model = new CreateGenreModel { Name = name };
            
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);// Hata obje sayısı 0 olmalı
        }
    }
}

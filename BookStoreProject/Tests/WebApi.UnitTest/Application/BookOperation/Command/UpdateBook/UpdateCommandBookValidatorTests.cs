using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using WebApi.UnitTest.TestSetup;
using Xunit;

namespace WebApi.UnitTest.Application.BookOperation.Command.UpdateBook
{
    public class UpdateCommandBookValidatorTests : IClassFixture<CommonTextFixture>
    {
        private readonly BookStoreDbContext context;
        public UpdateCommandBookValidatorTests(CommonTextFixture textFixture)
        {
            context = textFixture.context;
        }

        [Theory]
        [InlineData(1, "Lor", 1)]
        [InlineData(0, "Lord", 1)]
        [InlineData(1, "Lord O", -1)]
        [InlineData(0, "Lor", 0)]
        [InlineData(-1, "Lord Of", -1)]
        [InlineData(1, " ", 1)]
        [InlineData(1, "", 1)]
        public void WhenInvalidInputAreGiven_Validator_ShouldReturnErrors(int bookId, string title, int genreId)
        {
            // Arrange
            UpdateBookCommand command = new UpdateBookCommand(context);
            command.Model = new UpdateBookModel()
            {
                Title = title,
                GenreId = genreId
            };
            command.BookId = bookId;
            // Act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);
            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        // (: (: HAPPY PATH :) :)
        [Theory]
        [InlineData(1, 1, 1, "Lord Of The Rings")]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors(int bookId, int genreId, int authorId, string title)
        {
            UpdateBookCommand command = new UpdateBookCommand(context);
            command.Model = new UpdateBookModel()
            {
                Title = title,
                GenreId = genreId,
                AuthorId = authorId
            };
            command.BookId = bookId;
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);
            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}

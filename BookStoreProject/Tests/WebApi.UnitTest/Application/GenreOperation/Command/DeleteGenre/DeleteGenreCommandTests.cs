using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DBOperations;
using WebApi.UnitTest.TestSetup;
using Xunit;

namespace WebApi.UnitTest.Application.GenreOperation.Command.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly BookStoreDbContext context;
        public DeleteGenreCommandTests(CommonTextFixture textFixture)
        {
            context = textFixture.context;
        }
        [Fact]
        public void WhenAlreadyExistGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange
            DeleteGenreCommand command = new DeleteGenreCommand(context);
            
            // Act && Assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should()
                .Be("Book's genre not found.");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeDeleted()
        {
            // Arrange
            DeleteGenreCommand command = new DeleteGenreCommand(context);
            command.Id = 1;
            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            // Assert
            var genre = context.Genres.SingleOrDefault(g=>g.Id == command.Id);
            genre.Should().BeNull();
        }
    }
}

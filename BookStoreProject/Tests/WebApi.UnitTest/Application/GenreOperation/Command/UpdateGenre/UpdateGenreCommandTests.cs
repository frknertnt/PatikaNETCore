using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTest.TestSetup;
using Xunit;

namespace WebApi.UnitTest.Application.GenreOperation.Command.UpdateGenre
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly BookStoreDbContext context;
        public UpdateGenreCommandTests(CommonTextFixture textFixture)
        {
            context = textFixture.context;
        }
        [Fact]
        public void WhenAlreadyExistGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange
            UpdateGenreCommand command = new UpdateGenreCommand(context);
            command.Id = 0;

            // Act && Assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should()
                .Be("Book's genre not found.");
        }
        [Fact]
        public void WhenGivenNameIsSameWithAnotherGenre_InvalidOperationException_ShouldBeReturn()
        {
            var genre = new Genre() { Name = "Romance" };
            context.Genres.Add(genre);
            context.SaveChanges();

            UpdateGenreCommand command = new UpdateGenreCommand(context);
            command.Id = 2;
            command.Model = new UpdateGenreModel() { Name = "Romance" };

            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book's genre already exist.");
        }
        [Fact]
        public void WhenGivenBookIdinDB_Genre_ShouldBeUpdate()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(context);

            UpdateGenreModel model = new UpdateGenreModel() { Name = "WhenGivenBookIdinDB_Genre_ShouldBeUpdate" };
            command.Model = model;
            command.Id = 1;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var genre = context.Genres.SingleOrDefault(genre => genre.Id == command.Id);
            genre.Should().NotBeNull();

        }
    }
}

using FluentAssertions;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTest.TestSetup;
using Xunit;

namespace WebApi.UnitTest.Application.BookOperation.Command.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly BookStoreDbContext context;

        public DeleteBookCommandTests(CommonTextFixture textFixture)
        {
            context = textFixture.context;
        }

        [Fact]
        public void WhenAlreadyExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            DeleteBookCommand command = new DeleteBookCommand(context);

            // Act && Assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should()
                .Be("Book not found.");

        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            // Arrange
            var book = new Book()
            {
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = new DateTime(1980, 01, 05),
                GenreId = 1,
                AuthorId = 1,
            };
            context.Books.Add(book);
            context.SaveChanges();

            DeleteBookCommand command = new DeleteBookCommand(context);
            command.BookId = book.Id;

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            book = context.Books.SingleOrDefault(b=>b.Id == book.Id);
            book.Should().BeNull();  // Silinen kitap boş görünmeli
        }
    }
}

using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using WebApi.UnitTest.TestSetup;
using Xunit;

namespace WebApi.UnitTest.Application.BookOperation.Command.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly BookStoreDbContext context;
        public UpdateBookCommandTests(CommonTextFixture textFixture)
        {
            context = textFixture.context;
        }

        [Fact]  
        public void WhenAlreadyExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange
            UpdateBookCommand command = new UpdateBookCommand(context);
            command.BookId = 0;
            //Act && Assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should()
                .Be("Book not found.");
        }

        [Fact]
        public void WhenGivenBookIdInDatabase_Book_ShouldBeUpdate()
        {
            UpdateBookCommand command = new UpdateBookCommand(context);
            UpdateBookModel model = new UpdateBookModel()
            {
                Title = "Test_WhenGivenBookIdInDatabase_Book_ShouldBeUpdate",
                GenreId = 2
            };
            command.Model = model;
            command.BookId = 1;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var book = context.Books.SingleOrDefault(b=>b.Id == command.BookId);
            book.Should().NotBeNull();
        }
    }
}

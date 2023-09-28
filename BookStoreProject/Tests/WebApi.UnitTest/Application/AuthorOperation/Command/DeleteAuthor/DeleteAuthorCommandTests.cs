using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTest.TestSetup;
using Xunit;

namespace WebApi.UnitTest.Application.AuthorOperation.Command.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly BookStoreDbContext _context;


        public DeleteAuthorCommandTests(CommonTextFixture testFixture)
        {
            _context = testFixture.context;
        }


        [Fact]
        public void WhenGivenAuthorIdIsNotinDB_InvalidOperationException_ShouldBeReturn()
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.Id = 0;

            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author not found.");
        }


        [Fact]
        public void WhenGivenBookIdNotEqualAuthorId_InvalidOperationException_ShouldBeReturn()
        {

            var command = new DeleteAuthorCommand(_context);

            DeleteAuthorCommand commandd = new DeleteAuthorCommand(_context);
            command.Id = 1;

            FluentActions
                 .Invoking(() => command.Handle())
                 .Should().Throw<InvalidOperationException>().And.Message.Should().Be("The transaction cannot be completed while there is a book belonging to the author.");
        }


        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            //arrange
            var author = new Author() { FirstName = "Frank", LastName = "Rebart", BirthDate = new System.DateTime(1990, 05, 22) };
            _context.Add(author);
            _context.SaveChanges();

            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.Id = author.Id;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            author = _context.Authors.SingleOrDefault(x => x.Id == author.Id);
            author.Should().BeNull();

        }
    }
}

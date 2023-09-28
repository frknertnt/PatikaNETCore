using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DBOperations;
using WebApi.UnitTest.TestSetup;
using Xunit;

namespace WebApi.UnitTest.Application.AuthorOperation.Command.UpdateAuthor
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateAuthorCommandTests(CommonTextFixture testFixture)
        {
            _context = testFixture.context;
        }

        [Fact]
        public void WhenAlreadyExistAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange 
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.Id = 0;

            // act & asset 
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author not found.");

        }

        [Fact]
        public void WhenGivenAuthorIdinDB_Author_ShouldBeUpdate()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);

            UpdateAuthorModel model = new UpdateAuthorModel() 
            { 
                FirstName = "WhenGivenBookIdinDB_Book_ShouldBeUpdate", 
                LastName = "Martin" 
            };
            command.Model = model;
            command.Id = 1;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var author = _context.Authors.SingleOrDefault(author => author.Id == command.Id);
            author.Should().NotBeNull();

        }
    }
}

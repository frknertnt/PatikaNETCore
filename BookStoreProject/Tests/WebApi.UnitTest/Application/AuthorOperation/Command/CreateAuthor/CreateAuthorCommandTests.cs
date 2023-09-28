using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTest.TestSetup;
using Xunit;

namespace WebApi.UnitTest.Application.AuthorOperation.Command.CreateAuthor
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommandTests(CommonTextFixture testFixture)
        {
            _context = testFixture.context;
            _mapper = testFixture.mapper;
        }

        [Fact]
        public void WhenAlreadyExistAuthorTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange 
            var author = new Author() 
            {
                FirstName = "Furkan",
                LastName = "Ertantu",
                BirthDate = new DateTime(1964, 11, 19)
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = new CreateAuthorModel() 
            { 
                FirstName = author.FirstName, 
                LastName = author.LastName
            };
            // act & asset 
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should()
                .Be("Author is already exist.");

        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            CreateAuthorModel model = new CreateAuthorModel() 
            {
                FirstName = "Furkan",
                LastName = "Ertantu",
                BirthDate = new DateTime(1964, 11, 19)
            };
            command.Model = model;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var author = _context.Authors.SingleOrDefault(author => author.FirstName == model.FirstName && author.LastName == model.LastName);
            author.Should().NotBeNull();
            author.BirthDate.Should().Be((Convert.ToDateTime(model.BirthDate)));

        }




    }
}

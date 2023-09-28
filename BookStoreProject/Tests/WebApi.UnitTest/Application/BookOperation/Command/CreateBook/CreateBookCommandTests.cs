using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTest.TestSetup;
using Xunit;

namespace WebApi.UnitTest.Application.BookOperation.Command.CreateBook
{
    public class CreateBookCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;

        public CreateBookCommandTests(CommonTextFixture textFixture)
        {
            context = textFixture.context;
            mapper = textFixture.mapper;
        }

        [Fact] //tester
        public void WhenAlreadyExistTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            var book = new Book
            {
                Title = "Test_ WhenAlreadyExistTitleIsGiven_InvalidOperationException_ShouldBeReturn",
                PageCount = 100,
                PublishDate = new DateTime(1980, 01, 05),
                GenreId = 1,
                AuthorId = 1,
            };
            context.Books.Add(book);
            context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(context, mapper);
            command.Model = new CreateBookModel() { Title = book.Title };

            // Act && Assert (Run and validate)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book already exist.");
        }

        [Fact]
        public void WhenDateTimeIsNotEmptyGiven_Validator_ShouldBeReturnError()
        {
            //Arrange
            var book = new Book()
            {
                Title = "Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",
                PageCount = 100,
                PublishDate = new DateTime(2012, 11, 22),
                GenreId = 2,
                AuthorId = 1
            };
            context.Books.Add(book);
            context.SaveChanges();


            CreateBookCommand command = new CreateBookCommand(context, mapper);
            command.Model = new CreateBookModel() { AuthorId = book.Id };

            // Act & Assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("A book only have one author.");
        }

            // (: (: HAPPY PATH :) :)
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            // Arrange
            CreateBookCommand command = new CreateBookCommand(context, mapper);
            CreateBookModel model = new CreateBookModel()
            {
                Title = "Hobbit",
                PageCount = 1000,
                PublishDate = DateTime.Now.Date.AddYears(-10),
                GenreId = 1,
                AuthorId = 5
            };
            command.Model = model;

            //Act
            FluentActions.Invoking(() => command.Handle()).Invoke();//Should ile geri dönüş kontrol edildiği için otomatik çalışır
            // Fakat Should kullanılmazsa çalışması için Invoke yazmamız gerek 

            //Assert
            var book = context.Books.SingleOrDefault(book => book.Title == model.Title);
            book.Should().NotBeNull();
            book!.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
            book.AuthorId.Should().Be(model.AuthorId);
        }
    }
}

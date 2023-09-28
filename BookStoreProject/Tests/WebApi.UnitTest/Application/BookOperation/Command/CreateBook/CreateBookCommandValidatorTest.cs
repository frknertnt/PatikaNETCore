using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.UnitTest.TestSetup;
using Xunit;

namespace WebApi.UnitTest.Application.BookOperation.Command.CreateBook
{
    public class CreateBookCommandValidatorTest : IClassFixture<CommonTextFixture>
    {
        [Theory] // Her şekilde gelecek veri setini kolaylıkla test etmek için theory özelliğinden yararlandık
        [InlineData("Lord Of The Rings", 0, 0, 0)]//Test inputlarım
        [InlineData("Lord Of The Rings", 0, 0, 1)]
        [InlineData("Lord Of The Rings", 0, 1, 0)]
        [InlineData("Lord Of The Rings", 100, 0, 0)]
        [InlineData("", 0, 0, 0)]
        [InlineData("", 100, 0, 0)]
        [InlineData("Lord", 100, 0, 0)]
        [InlineData("Lord", 0, 1, 1)]
        [InlineData(" ", 100, 1, 1)]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId, int authorId)
        {
            // Arrange
            CreateBookCommand command = new CreateBookCommand(null!, null!);//null = metodu çalıştırmayacağım sadece inputlarıyla ilgileniyorum
            command.Model = new CreateBookModel()
            {
                Title = title,   //Test inputlarım
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-2),
                GenreId = genreId,
                AuthorId = authorId
            };

            // Act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);//Hata objesi 0dan büyük olmalı

        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null!, null!);
            command.Model = new CreateBookModel()
            {
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date, //DateTime şimdiki zaman verdik
                GenreId = 1,
                AuthorId = 1
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);

        }
        
                // (: (: HAPPY PATH :) :)
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null!, null!);
            command.Model = new CreateBookModel()
            {
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-2),
                GenreId = 1,
                AuthorId = 1
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);

        }
    }
}

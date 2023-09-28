using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTest.TestSetup;
using Xunit;

namespace WebApi.UnitTest.Application.GenreOperation.Command.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;

        public CreateGenreCommandTests(CommonTextFixture textFixture)
        {
            context = textFixture.context;
            mapper = textFixture.mapper;
        }

        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange (Preparation)
            var genre = new Genre()
            {
                Name = "TestWhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn",
            };
            context.Genres.Add(genre);
            context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(context, mapper);
            command.Model = new CreateGenreModel()
            {
                Name = genre.Name
            };

            // Act && Assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should()
                .Be("Book's genre already exist.");
        }
        
                // (: (: HAPPY PATH :) :)
        [Fact]  
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            CreateGenreCommand command = new CreateGenreCommand(context, mapper);
            command.Model = new CreateGenreModel()
            {
                Name = "TestWhenValidInputsAreGiven_Genre_ShouldBeCreated"
            };

            FluentActions .Invoking(() => command.Handle()).Invoke();

            var genre = context.Genres.SingleOrDefault(g => g.Name == command.Model.Name);
            genre.Should().NotBeNull();
        }
    }
}

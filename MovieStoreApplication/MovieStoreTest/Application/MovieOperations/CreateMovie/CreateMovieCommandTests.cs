using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.MovieOperations.CreateMovie;
using MovieStoreApi.DBOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.MovieOperations.CreateMovie
{
    public class CreateMovieCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;
        public CreateMovieCommandTests(CommonTestFixture testFixture)
        {
            _Context = testFixture.Context;
            _Mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistMovie_InvalidOperationException_ShouldBeReturn()
        {
            var director = new Entities.Director
            {
                Name = "WhenAlreadyExistMovie_InvalidOperationException_ShouldBeReturn",
                Surname = "WhenAlreadyExistMovie_InvalidOperationException_ShouldBeReturn"
            };
            _Context.Directors.Add(director);
            _Context.SaveChanges();
            var genre = new Entities.Genre
            {
                Name = "WhenAlreadyExistMovie_InvalidOperationException_ShouldBeReturn"
            };
            _Context.Genres.Add(genre);
            _Context.SaveChanges();
            var movie = new Entities.Movie
            {
                Name = "WhenAlreadyExistMovie_InvalidOperationException_ShouldBeReturn",
                Year = 2000,
                Price = 90,
                DirectorId = director.Id,
                GenreId=genre.Id
            };
            _Context.Movies.Add(movie);
            _Context.SaveChanges();

            CreateMovieCommand command = new CreateMovieCommand(_Context, _Mapper);
            command.Model = new CreateMovieModel() { Name = movie.Name, DirectorId = movie.DirectorId, GenreId = movie.GenreId, Price = movie.Price, Year = movie.Year };
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Eklemek istediğiniz film zaten mevcut");
            
        }
    }
}


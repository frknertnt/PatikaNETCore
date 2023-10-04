using System;
using System.Xml.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.MovieOperations.CreateMovie;
using MovieStoreApi.Application.MovieOperations.UpdateMovie;
using MovieStoreApi.DBOperations;
using MovieStoreApi.Entities;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.MovieOperations.UpdateMovie
{
    public class UpdateMovieCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _Context;
        public UpdateMovieCommandValidatorTests(CommonTestFixture testFixture)
        {
            _Context = testFixture.Context;
        }
        [Theory]
        [InlineData(1,"name", 2000, 1, 2, 0)]
        [InlineData(1, "", 2000, 1, 2, 20)]
        [InlineData(1, "name", 1800, 1, 2, 20)]
        [InlineData(1, "name", 2000, 0, 2, 20)]
        [InlineData(1, "name", 2000, 1, 0, 20)]
        [InlineData(0, "name", 2000, 1, 1, 20)]

        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int id, string name, int year, int genreId, int directorId, decimal price)
        {
            UpdateMovieModel model = new UpdateMovieModel()
            {
                Name = name,
                DirectorId = directorId,
                GenreId = genreId,
                Year = year,
                Price = price
            };

            UpdateMovieCommand command = new UpdateMovieCommand(_Context);
            command.Model = model;
            UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {
            var director = new Entities.Director
            {
                Name = "ForHappyCodeUpdateMovieValidator",
                Surname = "ForHappyCodeUpdateMovieValidator"
            };
            _Context.Directors.Add(director);
            _Context.SaveChanges();
            var genre = new Entities.Genre
            {
                Name = "ForHappyCodeUpdateMovieValidator"
            };
            _Context.Genres.Add(genre);
            _Context.SaveChanges();
            var movie = new Entities.Movie
            {
                Name = "ForHappyCodeUpdateMovieValidator",
                Year = 2000,
                DirectorId = director.Id,
                GenreId = genre.Id,
                Price = 200
            };
            _Context.Movies.Add(movie);
            _Context.SaveChanges();

            UpdateMovieModel model = new UpdateMovieModel()
            {
                Name = "UpdateValidatorTest",
                DirectorId = director.Id,
                GenreId = genre.Id,
                Year = 2004,
                Price = 300
            };
            var movieId = movie.Id;
            UpdateMovieCommand command = new UpdateMovieCommand(_Context);
            command.Model = model;
            command.MovieId = movieId;
            UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}


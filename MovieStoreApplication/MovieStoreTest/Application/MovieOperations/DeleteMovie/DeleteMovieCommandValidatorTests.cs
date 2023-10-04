using System;
using FluentAssertions;
using MovieStoreApi.Application.MovieOperations.DeleteMovie;
using MovieStoreApi.DBOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.MovieOperations.DeleteMovie
{
    public class DeleteMovieCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _Context;
        public DeleteMovieCommandValidatorTests(CommonTestFixture testFixture)
        {
            _Context = testFixture.Context;
        }
        [Fact]
        public void WhenActorIdLessThanZero_Validator_ShouldBeReturnError()
        {
            DeleteMovieCommand command = new DeleteMovieCommand(_Context);
            command.MovieId = 0;

            DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {
            var director = new Entities.Director
            {
                Name = "ForHappyCodeMovieValidator",
                Surname = "ForHappyCodeMovieValidator"
            };
            _Context.Directors.Add(director);
            _Context.SaveChanges();
            var genre = new Entities.Genre
            {
                Name = "ForHappyCodeMovieValidator"
            };
            _Context.Genres.Add(genre);
            _Context.SaveChanges();
            var movie = new Entities.Movie
            {
                Name = "ForHappyCodeMovieValidator",
                Year = 2000,
                DirectorId = director.Id,
                GenreId = genre.Id,
                Price = 200
            };
            _Context.Movies.Add(movie);
            _Context.SaveChanges();

            var movieId = movie.Id;
            DeleteMovieCommand command = new DeleteMovieCommand(_Context);
            command.MovieId = movieId;

            DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}


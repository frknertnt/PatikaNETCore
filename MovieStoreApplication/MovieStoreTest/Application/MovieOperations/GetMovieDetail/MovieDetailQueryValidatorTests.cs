using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.MovieOperations.GetMovieDetail;
using MovieStoreApi.DBOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.MovieOperations.GetMovieDetail
{
    public class MovieDetailQueryValidatorTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;
        public MovieDetailQueryValidatorTests(CommonTestFixture testFixture)
        {
            _Context = testFixture.Context;
            _Mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenMovieIdLessThanZero_Validator_ShouldBeReturnError()
        {
            MovieDetailQuery query = new MovieDetailQuery(_Context, _Mapper);
            query.MovieId = 0;

            MovieDetailQueryValidator validator = new MovieDetailQueryValidator();
            var result = validator.Validate(query);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {
            var director = new Entities.Director
            {
                Name = "ForHappyCodeMovieDetailQueryTests",
                Surname = "ForHappyCodeMovieDetailQueryTests"
            };
            _Context.Directors.Add(director);
            _Context.SaveChanges();
            var genre = new Entities.Genre
            {
                Name = "ForHappyCodeMovieDetailQueryTests"
            };
            _Context.Genres.Add(genre);
            _Context.SaveChanges();
            var movie = new Entities.Movie
            {
                Name = "ForHappyCodeMovieDetailQueryTests",
                Year = 2000,
                DirectorId = director.Id,
                GenreId = genre.Id,
                Price = 200
            };
            _Context.Movies.Add(movie);
            _Context.SaveChanges();

            var movieId = movie.Id;
            MovieDetailQuery query = new MovieDetailQuery(_Context, _Mapper);
            query.MovieId = movieId;

            MovieDetailQueryValidator validator = new MovieDetailQueryValidator();
            var result = validator.Validate(query);
            result.Errors.Count.Should().Be(0);
        }
    }
}


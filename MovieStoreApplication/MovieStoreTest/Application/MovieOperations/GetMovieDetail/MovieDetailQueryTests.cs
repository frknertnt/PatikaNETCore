using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.MovieOperations.GetMovieDetail;
using MovieStoreApi.DBOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.MovieOperations.GetMovieDetail
{
    public class MovieDetailQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;
        public MovieDetailQueryTests(CommonTestFixture testFixture)
        {
            _Context = testFixture.Context;
            _Mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenTheMovieIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            var director = new Entities.Director
            {
                Name = "MovieDetailQueryTests",
                Surname = "MovieDetailQueryTests"
            };
            _Context.Directors.Add(director);
            _Context.SaveChanges();
            var genre = new Entities.Genre
            {
                Name = "MovieDetailQueryTests"
            };
            _Context.Genres.Add(genre);
            _Context.SaveChanges();
            var movie = new Entities.Movie
            {
                Name = "MovieDetailQueryTests",
                Year = 2000,
                DirectorId = director.Id,
                GenreId = genre.Id,
                Price = 200
            };
            _Context.Movies.Add(movie);
            _Context.SaveChanges();

            var movieId = movie.Id;

            _Context.Remove(movie);
            _Context.SaveChanges();

            MovieDetailQuery query = new MovieDetailQuery(_Context, _Mapper);
            query.MovieId = movie.Id;

            FluentActions
                .Invoking(() => query.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film mevcut değil");
        }
        [Fact]
        public void WhenTheMovieIsNotAvailable_Actor_ShouldNotBeReturnErrors()
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
            query.MovieId = movie.Id;

            FluentActions
                .Invoking(() => query.Handle().GetAwaiter().GetResult()).Invoke();
        }
    }
}


using System;
using FluentAssertions;
using MovieStoreApi.Application.MovieOperations.DeleteMovie;
using MovieStoreApi.DBOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.MovieOperations.DeleteMovie
{
    public class DeleteMovieCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _Context;
        public DeleteMovieCommandTests(CommonTestFixture testFixture)
        {
            _Context = testFixture.Context;
        }
        [Fact]
        public void WhenTheMovieIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            var director = new Entities.Director
            {
                Name = "WhenTheMovieIsNotAvailable_InvalidOperationException_ShouldBeReturn",
                Surname = "WhenTheMovieIsNotAvailable_InvalidOperationException_ShouldBeReturn"
            };
            _Context.Directors.Add(director);
            _Context.SaveChanges();
            var genre = new Entities.Genre
            {
                Name = "WhenTheMovieIsNotAvailable_InvalidOperationException_ShouldBeReturn"
            };
            _Context.Genres.Add(genre);
            _Context.SaveChanges();
            var movie = new Entities.Movie
            {
                Name = "WhenTheMovieIsNotAvailable_InvalidOperationException_ShouldBeReturn",
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

            DeleteMovieCommand command = new DeleteMovieCommand(_Context);
            command.MovieId = movieId;
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silmek istediğiniz film mevcut değil");
        }
        [Fact]
        public void WhenValidInputsAreGiven_DeleteMovie_ShouldNotBeReturnError()
        {
            var director = new Entities.Director
            {
                Name = "ForHappyCodeMovie",
                Surname = "ForHappyCodeMovie"
            };
            _Context.Directors.Add(director);
            _Context.SaveChanges();
            var genre = new Entities.Genre
            {
                Name = "ForHappyCodeMovie"
            };
            _Context.Genres.Add(genre);
            _Context.SaveChanges();
            var movie = new Entities.Movie
            {
                Name = "ForHappyCodeMovie",
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

            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();
        }
    }
}


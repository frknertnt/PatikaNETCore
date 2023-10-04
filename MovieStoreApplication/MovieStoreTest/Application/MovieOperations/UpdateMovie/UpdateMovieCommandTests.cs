using System;
using FluentAssertions;
using MovieStoreApi.Application.MovieOperations.DeleteMovie;
using MovieStoreApi.Application.MovieOperations.UpdateMovie;
using MovieStoreApi.DBOperations;
using MovieStoreApi.Entities;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.MovieOperations.UpdateMovie
{
    public class UpdateMovieCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _Context;
        public UpdateMovieCommandTests(CommonTestFixture testFixture)
        {
            _Context = testFixture.Context;
        }
        [Fact]
        public void WhenTheMovieIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            var director = new Entities.Director
            {
                Name = "WhenTheMovieIsNotAvailable_InvalidOperationException_ShouldBeReturnUpdateMovie",
                Surname = "WhenTheMovieIsNotAvailable_InvalidOperationException_ShouldBeReturnUpdateMovie"
            };
            _Context.Directors.Add(director);
            _Context.SaveChanges();
            var genre = new Entities.Genre
            {
                Name = "WhenTheMovieIsNotAvailable_InvalidOperationException_ShouldBeReturnUpdateMovie"
            };
            _Context.Genres.Add(genre);
            _Context.SaveChanges();
            var movie = new Entities.Movie
            {
                Name = "WhenTheMovieIsNotAvailable_InvalidOperationException_ShouldBeReturnUpdateMovie",
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

            UpdateMovieCommand command = new UpdateMovieCommand(_Context);
            command.MovieId = movieId;
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellemek istediğiniz film mevcut değil");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Movie_ShouldBeUpdated()
        {
            var director = new Entities.Director
            {
                Name = "ForHappyCodeUpdateMovie",
                Surname = "ForHappyCodeUpdateMovie"
            };
            _Context.Directors.Add(director);
            _Context.SaveChanges();
            var actor = new Actor
            {
                Name = "ForHappyCodeUpdateMovie",
                Surname = "ForHappyCodeUpdateMovie"
            };
            _Context.Actors.Add(actor);
            _Context.SaveChanges();
            var genre = new Entities.Genre
            {
                Name = "ForHappyCodeUpdateMovie"
            };
            _Context.Genres.Add(genre);
            _Context.SaveChanges();
            var movie = new Entities.Movie
            {
                Name = "ForHappyCodeUpdateMovie",
                Year = 2001,
                DirectorId = director.Id,
                GenreId = genre.Id,
                Price = 200
            };
            _Context.Movies.Add(movie);
            _Context.SaveChanges();

            var movieId = movie.Id;
            UpdateMovieModel model = new UpdateMovieModel
            {
                Name = "UpdateTest",
                Year = 2009,
                DirectorId = director.Id,
                GenreId = genre.Id,
                Price = 308,
                Actors=new[] {actor.Id}
            };

            UpdateMovieCommand command = new UpdateMovieCommand(_Context);
            command.MovieId = movieId;
            command.Model = model;
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();
            movie = _Context.Movies.FirstOrDefault(c => c.Id == command.MovieId);
            movie.Name.Should().Be(model.Name);
            movie.DirectorId.Should().Be(model.DirectorId);
            movie.GenreId.Should().Be(model.GenreId);
            movie.Year.Should().Be(model.Year);
            movie.Price.Should().Be(model.Price);
        }
    }
}


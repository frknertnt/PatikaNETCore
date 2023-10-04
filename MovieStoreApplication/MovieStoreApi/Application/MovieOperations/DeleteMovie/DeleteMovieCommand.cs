using System;
using MovieStoreApi.DBOperations;

namespace MovieStoreApi.Application.MovieOperations.DeleteMovie
{
    public class DeleteMovieCommand
    {
        public int MovieId { get; set; }
        private readonly MovieStoreDbContext _Context;
        public DeleteMovieCommand(MovieStoreDbContext Context)
        {
            _Context = Context;
        }
        public async Task Handle()
        {
            var movie = _Context.Movies.FirstOrDefault(c => c.Id == MovieId && !c.IsDeleted);
            if (movie == null)
                throw new InvalidOperationException("Silmek istediğiniz film mevcut değil");

            movie.IsDeleted = true;
            //_Context.Movies.Remove(movie);
            await _Context.SaveChangesAsync();
        }
    }
}


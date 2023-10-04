using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreApi.DBOperations;
using MovieStoreApi.Entities;

namespace MovieStoreApi.Application.MovieOperations.CreateMovie
{
    public class CreateMovieCommand
    {
        public CreateMovieModel Model { get; set; }
        private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;

        public CreateMovieCommand(MovieStoreDbContext Context, IMapper Mapper)
        {
            _Context = Context;
            _Mapper = Mapper;
        }
        public async Task Handle()
        {
            var movie = _Context.Movies.Where(c => !c.IsDeleted).FirstOrDefault(c => c.Name == Model.Name && c.DirectorId == Model.DirectorId);
            if (movie is not null)
                throw new InvalidOperationException("Eklemek istediğiniz film zaten mevcut");
            movie = _Mapper.Map<Movie>(Model);
            _Context.Movies.Add(movie);
            foreach (var a in movie.Actors)
                _Context.Entry(a).State = EntityState.Unchanged;
            await _Context.SaveChangesAsync();
        }
    }

    public class CreateMovieModel
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
        public IEnumerable<int> Actors { get; set; }
        public decimal Price { get; set; }
    }
}


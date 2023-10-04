using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreApi.DBOperations;
using MovieStoreApi.Entities;

namespace MovieStoreApi.Application.MovieOperations.GetMovieDetail
{
    public class MovieDetailQuery
    {
        public MovieViewModel Model { get; set; }
        public int MovieId { get; set; }
        private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;
        public MovieDetailQuery(MovieStoreDbContext Context, IMapper Mapper)
        {
            _Context = Context;
            _Mapper = Mapper;
        }

        public async Task<MovieViewModel> Handle()
        {
            var movie = _Context.Movies.Where(c=> !c.IsDeleted).Include(c=> c.Genre).Include(c => c.Director).Include(c => c.Actors).FirstOrDefault(c => c.Id == MovieId);
            if (movie == null)
                throw new InvalidOperationException("Film mevcut değil");

            Model = _Mapper.Map<MovieViewModel>(movie);
            return Model;
        }

    }
    public class MovieViewModel
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public ICollection<string> Actors { get; set; }
        public decimal Price { get; set; }
    }
}


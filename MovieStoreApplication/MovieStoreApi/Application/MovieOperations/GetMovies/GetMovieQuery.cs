using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreApi.DBOperations;

namespace MovieStoreApi.Application.MovieOperations.GetMovies
{
    public class GetMovieQuery
    {
        private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;
        public GetMovieQuery(MovieStoreDbContext Context, IMapper Mapper)
        {
            _Context = Context;
            _Mapper = Mapper;
        }

        public async Task<List<MoviesViewModel>> Handle()
        {
            var movieList = await _Context.Movies.Where(c=> !c.IsDeleted).Include(c=> c.Actors).Include(c=> c.Genre).Include(c=>c.Director).OrderBy(c => c.Id).ToListAsync();
            List<MoviesViewModel> vm = _Mapper.Map<List<MoviesViewModel>>(movieList);

            return vm;
        }
    }
    public class MoviesViewModel
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public ICollection<string> Actors { get; set; }
        public decimal Price { get; set; }
    }
}


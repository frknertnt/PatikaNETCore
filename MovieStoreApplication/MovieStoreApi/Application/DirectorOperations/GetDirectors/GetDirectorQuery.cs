using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreApi.Application.DirectorOperations.GetDirectorDetail;
using MovieStoreApi.DBOperations;
using MovieStoreApi.Entities;

namespace MovieStoreApi.Application.DirectorOperations.GetDirectors
{
    public class GetDirectorQuery
    {
        public DirectorsViewModel Model { get; set; }
        private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;
        public GetDirectorQuery(MovieStoreDbContext Context, IMapper Mapper)
        {
            _Context = Context;
            _Mapper = Mapper;
        }
        public async Task<List<DirectorsViewModel>> Handle()
        {
            var directorList = await _Context.Directors.Include(c => c.Movies).OrderBy(c => c.Id).ToListAsync();
            List<DirectorsViewModel> vm = _Mapper.Map<List<DirectorsViewModel>>(directorList);

            return vm;

        }
    }

    public class DirectorsViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<string> Movies { get; set; }
    }
}


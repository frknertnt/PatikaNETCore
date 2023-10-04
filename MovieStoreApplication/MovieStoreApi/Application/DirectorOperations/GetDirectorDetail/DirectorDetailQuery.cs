using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreApi.Application.DirectorOperations.CreateDirector;
using MovieStoreApi.DBOperations;
using MovieStoreApi.Entities;

namespace MovieStoreApi.Application.DirectorOperations.GetDirectorDetail
{
    public class DirectorDetailQuery
    {
        public int DirectorId { get; set; }
        public DirectorViewModel Model { get; set; }
        private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;
        public DirectorDetailQuery(MovieStoreDbContext Context, IMapper Mapper)
        {
            _Context = Context;
            _Mapper = Mapper;
        }
        public async Task<DirectorViewModel> Handle()
        {
            var director = _Context.Directors.Include(c=> c.Movies).FirstOrDefault(c => c.Id == DirectorId);
            if (director == null)
                throw new InvalidOperationException("Yönetmen mevcut değil");

            Model = _Mapper.Map<DirectorViewModel>(director);
            return Model;
        }
    }

    public class DirectorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<string> Movies { get; set; }
    }
}


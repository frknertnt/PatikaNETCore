using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreApi.DBOperations;
using MovieStoreApi.Entities;

namespace MovieStoreApi.Application.DirectorOperations.CreateDirector
{
    public class CreateDirectorCommand
    {
        public CreateDirectorModel Model { get; set; }
        private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;
        public CreateDirectorCommand(MovieStoreDbContext Context, IMapper Mapper)
        {
            _Context = Context;
            _Mapper = Mapper;
        }

        public async Task Handle()
        {
            var director = _Context.Directors.FirstOrDefault(c => c.Name == Model.Name && c.Surname == Model.Surname);
            if (director is not null)
                throw new InvalidOperationException("Eklemek istediğiniz yönetmen mevcut");

            director = _Mapper.Map<Director>(Model);
            _Context.Add(director);
            await _Context.SaveChangesAsync();
        }
    }
    public class CreateDirectorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}


using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreApi.Application.DirectorOperations.CreateDirector;
using MovieStoreApi.DBOperations;
using MovieStoreApi.Entities;

namespace MovieStoreApi.Application.DirectorOperations.DeleteDirector
{
    public class DeleteDirectorCommand
    {
        public int DirectorId { get; set; }
        private readonly MovieStoreDbContext _Context;
        public DeleteDirectorCommand(MovieStoreDbContext Context)
        {
            _Context = Context;
        }
        public async Task Handle()
        {
            var director = _Context.Directors.Include(c=>c.Movies).FirstOrDefault(c => c.Id == DirectorId);
            if (director == null)
                throw new InvalidOperationException("Silmek istediğiniz yönetmen mevcut değil");

            if (director.Movies.Any())
                throw new InvalidOperationException("Silmek istediğiniz yönetmenin filmi mevcut");

            _Context.Remove(director);
            await _Context.SaveChangesAsync();
        }
    }
}


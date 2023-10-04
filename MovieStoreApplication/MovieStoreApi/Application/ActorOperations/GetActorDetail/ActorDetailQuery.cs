﻿using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreApi.DBOperations;
using MovieStoreApi.Entities;

namespace MovieStoreApi.Application.ActorOperations.GetActorDetail
{
    public class ActorDetailQuery
    {
        public ActorViewModel Model { get; set; }
        public int ActorId { get; set; }
        private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;
        public ActorDetailQuery(MovieStoreDbContext Context, IMapper Mapper)
        {
            _Context = Context;
            _Mapper = Mapper;
        }
        public async Task<ActorViewModel> Handle()
        {
            var actor = _Context.Actors.Include(c=> c.Movies).FirstOrDefault(c => c.Id == ActorId);
            if (actor == null)
                throw new InvalidOperationException("Aktör mevcut değil");

            Model = _Mapper.Map<ActorViewModel>(actor);
            return Model;
        }
    }
    public class ActorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<string> Movies { get; set; }
    }
}

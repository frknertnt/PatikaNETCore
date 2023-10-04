using System;
using AutoMapper;
using MovieStoreApi.DBOperations;

namespace MovieStoreApi.Application.ActorOperations.DeleteActor
{
    public class DeleteActorCommand
    {
        public int ActorId { get; set; }
        private readonly MovieStoreDbContext _Context;
        private IMovieStoreDbContext Context;

        public DeleteActorCommand(MovieStoreDbContext Context)
        {
            _Context = Context;
        }

        public async Task Handle()
        {
            var actor = _Context.Actors.FirstOrDefault(c => c.Id == ActorId);
            if (actor == null)
                throw new InvalidOperationException("Silmek istediğiniz aktör mevcut değil");

            _Context.Actors.Remove(actor);
            await _Context.SaveChangesAsync();
        }
    }
}


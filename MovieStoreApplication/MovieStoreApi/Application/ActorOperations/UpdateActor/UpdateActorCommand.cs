using System;
using MovieStoreApi.DBOperations;

namespace MovieStoreApi.Application.ActorOperations.UpdateActor
{
    public class UpdateActorCommand
    {
        public UpdateActorModel Model { get; set; }
        public int ActorId { get; set; }
        private readonly MovieStoreDbContext _Context;
        public UpdateActorCommand(MovieStoreDbContext Context)
        {
            _Context = Context;
        }
        public async Task Handle()
        {
            var actor = _Context.Actors.FirstOrDefault(c => c.Id == ActorId);
            if (actor == null)
                throw new InvalidOperationException("Güncellemek istediğiniz aktör bulunamadı");

            actor.Name = Model.Name != default ? Model.Name : actor.Name;
            actor.Surname = Model.Surname != default ? Model.Surname : actor.Surname;
            await _Context.SaveChangesAsync();
        }
    }
    public class UpdateActorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}


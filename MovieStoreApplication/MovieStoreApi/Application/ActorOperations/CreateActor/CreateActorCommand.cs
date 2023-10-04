using AutoMapper;
using MovieStoreApi.Application.ActorOperations.CreateActor;
using MovieStoreApi.DBOperations;
using MovieStoreApi.Entities;

namespace MovieStoreApi.Application.ActorOperations.CreateActor
{
    public class CreateActorCommand
    {
        public CreateActorModel Model { get; set; }
        private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;
        public CreateActorCommand(MovieStoreDbContext Context, IMapper Mapper)
        {
            _Context = Context;
            _Mapper = Mapper;
        }
        public async Task Handle ()
        {
            var actor = _Context.Actors.FirstOrDefault(c => c.Name == Model.Name && c.Surname == Model.Surname);
            if (actor is not null)
                throw new InvalidOperationException("Eklemek istediğiniz aktör zaten var");

            actor = _Mapper.Map<Actor>(Model);
            _Context.Actors.Add(actor);
            await _Context.SaveChangesAsync();
        }
    }

    public class CreateActorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}


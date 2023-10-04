using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreApi.DBOperations;

namespace MovieStoreApi.Application.ActorOperations.GetActors
{
    public class GetActorQuery
    {
        public ActorsViewModel Model { get; set; }
        public int ActorId { get; set; }
        private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;
        public GetActorQuery(MovieStoreDbContext Context, IMapper Mapper)
        {
            _Context = Context;
            _Mapper = Mapper;
        }
        public async Task<List<ActorsViewModel>> Handle()
        {
            var actorList = _Context.Actors.Include(c=> c.Movies).OrderBy(c => c.Id).ToList();

            List<ActorsViewModel> vm = _Mapper.Map<List<ActorsViewModel>>(actorList);
            return vm;
        }
    }

    public class ActorsViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<string> Movies { get; set; }
    }
}


using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStoreApi.Application.ActorOperations.CreateActor;
using MovieStoreApi.Application.ActorOperations.DeleteActor;
using MovieStoreApi.Application.ActorOperations.GetActorDetail;
using MovieStoreApi.Application.ActorOperations.GetActors;
using MovieStoreApi.Application.ActorOperations.UpdateActor;
using MovieStoreApi.DBOperations;

namespace MovieStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;
        public ActorController(MovieStoreDbContext Context, IMapper Mapper)
        {
            _Context = Context;
            _Mapper = Mapper;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            GetActorQuery query = new(_Context, _Mapper);
            var result = await query.Handle();
            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ActorDetailQuery query = new(_Context, _Mapper);
            query.ActorId = id;

            ActorDetailQueryValidator validator = new ActorDetailQueryValidator();
            validator.ValidateAndThrow(query);

            var result = await query.Handle();
            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateActorModel newActor)
        {
            CreateActorCommand command = new(_Context, _Mapper);
            command.Model = newActor;
            CreateActorCommandValidator validator = new CreateActorCommandValidator();
            validator.ValidateAndThrow(command);

            await command.Handle();
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateActorModel updateActor)
        {
            UpdateActorCommand command = new(_Context);
            command.ActorId = id;
            command.Model = updateActor;
            UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
            validator.ValidateAndThrow(command);

            await command.Handle();
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteActorCommand command = new(_Context);
            command.ActorId = id;
            DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
            validator.ValidateAndThrow(command);

            await command.Handle();
            return Ok();
        }
    }
}

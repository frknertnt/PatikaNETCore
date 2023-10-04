using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStoreApi.Application.DirectorOperations.CreateDirector;
using MovieStoreApi.Application.DirectorOperations.DeleteDirector;
using MovieStoreApi.Application.DirectorOperations.GetDirectorDetail;
using MovieStoreApi.Application.DirectorOperations.GetDirectors;
using MovieStoreApi.Application.DirectorOperations.UpdateDirector;
using MovieStoreApi.DBOperations;

namespace MovieStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;
        public DirectorController(MovieStoreDbContext Context, IMapper Mapper)
        {
            _Context = Context;
            _Mapper = Mapper;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            GetDirectorQuery query = new(_Context, _Mapper);
            var result = await query.Handle();
            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            DirectorDetailQuery query = new(_Context, _Mapper);
            query.DirectorId = id;
            DirectorDetailQueryValidator validator = new DirectorDetailQueryValidator();
            validator.ValidateAndThrow(query);
            var result = await query.Handle();
            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateDirectorModel newDirector)
        {
            CreateDirectorCommand command = new(_Context, _Mapper);
            command.Model = newDirector;
            CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
            validator.ValidateAndThrow(command);
            await command.Handle();

            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateDirectorModel updateDirector)
        {
            UpdateDirectorCommand command = new(_Context);
            command.Model = updateDirector;
            command.DirectorId = id;
            UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
            validator.ValidateAndThrow(command);

            await command.Handle();
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteDirectorCommand command = new(_Context);
            command.DirectorId = id;
            DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
            validator.ValidateAndThrow(command);

            await command.Handle();
            return Ok();
        }
    }
}

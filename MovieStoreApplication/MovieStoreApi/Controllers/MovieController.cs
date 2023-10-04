using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStoreApi.Application.MovieOperations.CreateMovie;
using MovieStoreApi.Application.MovieOperations.DeleteMovie;
using MovieStoreApi.Application.MovieOperations.GetMovieDetail;
using MovieStoreApi.Application.MovieOperations.GetMovies;
using MovieStoreApi.Application.MovieOperations.UpdateMovie;
using MovieStoreApi.DBOperations;

namespace MovieStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;
        public MovieController(MovieStoreDbContext Context, IMapper Mapper)
        {
            _Context = Context;
            _Mapper = Mapper;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            GetMovieQuery query = new(_Context, _Mapper);
            var result = await query.Handle();

            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            MovieDetailQuery query = new(_Context, _Mapper);
            query.MovieId = id;
            MovieDetailQueryValidator validator = new MovieDetailQueryValidator();
            validator.ValidateAndThrow(query);
            var result = await query.Handle();
            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateMovieModel newMovie)
        {
            CreateMovieCommand command = new(_Context, _Mapper);
            command.Model = newMovie;
            CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
            validator.ValidateAndThrow(command);
            await command.Handle();

            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateMovieModel updateMovie)
        {
            UpdateMovieCommand update = new(_Context);
            update.Model = updateMovie;
            update.MovieId = id;
            UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
            validator.ValidateAndThrow(update);
            await update.Handle();

            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteMovieCommand command = new(_Context);
            command.MovieId = id;
            DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
            validator.ValidateAndThrow(command);
            await command.Handle();
            return Ok();
        }
    }
}

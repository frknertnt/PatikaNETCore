using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStoreApi.Application.CustomerOperations.CreateCustomer;
using MovieStoreApi.DBOperations;
using MovieStoreApi.TokenOperations.Models;
using static MovieStoreApi.Application.CustomerOperations.CreateCustomer.CreateTokenCommand;

namespace MovieStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;
        private readonly IConfiguration _configuration;

        public CustomerController(IMapper Mapper, MovieStoreDbContext Context, IConfiguration configuration)
        {
            _Mapper = Mapper;
            _Context = Context;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCustomerModel newCustomer)
        {
            CreateCustomerCommand command = new CreateCustomerCommand(_Context, _Mapper);
            command.Model = newCustomer;
            CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
            validator.ValidateAndThrow(command);

            await command.Handle();
            return Ok();
        }
        [HttpPost("connect/token")]
        public async Task<ActionResult<Token>> CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command = new CreateTokenCommand(_Context, _Mapper, _configuration);
            command.Model = login;
            var token = await command.Handle();
            return token;
        }
        [HttpGet("refreshToken")]
        public async Task<ActionResult<Token>> RefreshToken([FromQuery] string token)
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_Context, _configuration);
            command.RefreshToken = token;
            var resultToken = await command.Handle();
            return resultToken;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

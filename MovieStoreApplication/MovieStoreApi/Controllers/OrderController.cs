using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStoreApi.Application.OrderOperations.CreateOrder;
using MovieStoreApi.Application.OrderOperations.GetOrders;
using MovieStoreApi.DBOperations;

namespace MovieStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;

        public OrderController(MovieStoreDbContext Context, IMapper Mapper)
        {
            _Mapper = Mapper;
            _Context = Context;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            GetOrderQuery query = new GetOrderQuery(_Context, _Mapper);
            var result = await query.Handle();
            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateOrderModel newOrder)
        {
            CreateOrderCommand command = new CreateOrderCommand(_Context, _Mapper);
            command.Model = newOrder;
            CreateOrderCommandValidator validator = new CreateOrderCommandValidator();
            validator.ValidateAndThrow(command);
            await command.Handle();

            return Ok();
        }
    }
}

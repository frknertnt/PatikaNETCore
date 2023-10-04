using System;
using AutoMapper;
using MovieStoreApi.DBOperations;
using MovieStoreApi.Entities;

namespace MovieStoreApi.Application.OrderOperations.CreateOrder
{
	public class CreateOrderCommand
	{
        public CreateOrderModel Model { get; set; }
		private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;


        public CreateOrderCommand(MovieStoreDbContext Context, IMapper Mapper)
        {
            _Context = Context;
			_Mapper = Mapper;
        }
        public async Task Handle()
        {
            var order = _Mapper.Map<Order>(Model);
            _Context.Orders.Add(order);
            await _Context.SaveChangesAsync();
        }
	}
    public class CreateOrderModel
    {
        public int CustomerId { get; set; }
        public int MovieId { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchasedDate { get; set; }
    }
}


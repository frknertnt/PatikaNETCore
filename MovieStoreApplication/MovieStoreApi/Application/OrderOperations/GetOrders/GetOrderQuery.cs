using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreApi.DBOperations;
using MovieStoreApi.Entities;

namespace MovieStoreApi.Application.OrderOperations.GetOrders
{
	public class GetOrderQuery
	{
		private readonly MovieStoreDbContext _Context;
		private readonly IMapper _Mapper;
		public GetOrderQuery(MovieStoreDbContext Context, IMapper Mapper)
		{
			_Context = Context;
			_Mapper = Mapper;
		}
		public async Task<List<OrderViewModel>> Handle()
		{
			var orderList = await _Context.Orders.Include(c=> c.PurchasedMovie).Include(c=> c.Customer).OrderBy(c => c.Id).ToListAsync();
			if (orderList == null)
				throw new InvalidOperationException("Sipariş listesi mevcut değil.");

			List<OrderViewModel> vm = _Mapper.Map<List<OrderViewModel>>(orderList);
			return vm;
        }
	}
	public class OrderViewModel
	{
        public string CustomerName { get; set; }
        public string PurchasedMovie { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchasedDate { get; set; }
    }
}


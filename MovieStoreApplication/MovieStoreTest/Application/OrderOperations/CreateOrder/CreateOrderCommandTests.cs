using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.OrderOperations.CreateOrder;
using MovieStoreApi.DBOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.OrderOperations.CreateOrder
{
	public class CreateOrderCommandTests:IClassFixture<CommonTestFixture>
	{
		private readonly MovieStoreDbContext _Context;
		private readonly IMapper _Mapper;
		public CreateOrderCommandTests(CommonTestFixture testFixture)
		{
			_Context = testFixture.Context;
			_Mapper = testFixture.Mapper;
		}
		[Fact]
		public void WhenAlreadyExistOrder_InvalidOperationException_ShouldBeReturn()
		{
        
            var customer = new Entities.Customer
            {
                Name = "WhenAlreadyExistCustomer_InvalidOperationException_ShouldBeReturn",
                Surname = "WhenAlreadyExistCustomer_InvalidOperationException_ShouldBeReturn"
            };
            _Context.Customers.Add(customer);
            _Context.SaveChanges();
            var order = new Entities.Order
            {
                CustomerId = customer.Id,
                MovieId = 1
            };
			_Context.Orders.Add(order);
			_Context.SaveChanges();
            CreateOrderCommand command = new CreateOrderCommand(_Context, _Mapper);
            command.Model = new CreateOrderModel { MovieId = 1, CustomerId = customer.Id };
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();		}

    }
}


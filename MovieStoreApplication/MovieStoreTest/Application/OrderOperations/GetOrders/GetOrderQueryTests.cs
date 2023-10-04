using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.OrderOperations.GetOrders;
using MovieStoreApi.DBOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.OrderOperations.GetOrders
{
	public class GetOrderQueryTests:IClassFixture<CommonTestFixture>
	{
        private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;
        public GetOrderQueryTests(CommonTestFixture testFixture)
        {
            _Context = testFixture.Context;
            _Mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenQueryGetResult_Order_ShouldNotBeReturnErrors()
        {
            GetOrderQuery query = new GetOrderQuery(_Context, _Mapper);
            FluentActions
                .Invoking(() => query.Handle().GetAwaiter().GetResult()).Invoke();
        }
    }
}


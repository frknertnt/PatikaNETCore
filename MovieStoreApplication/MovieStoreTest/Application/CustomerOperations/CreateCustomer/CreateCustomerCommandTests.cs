using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.CustomerOperations.CreateCustomer;
using MovieStoreApi.DBOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.CustomerOperations.CreateCustomer
{
    public class CreateCustomerCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;
        public CreateCustomerCommandTests(CommonTestFixture testFixture)
        {
            _Context = testFixture.Context;
            _Mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistCustomer_InvalidOperationException_ShouldBeReturn()
        {
            var genre = new Entities.Genre
            {
                Name = "WhenAlreadyExistCustomer_InvalidOperationException_ShouldBeReturn"
            };
            _Context.Genres.Add(genre);
            var customer = new Entities.Customer
            {
                Name = "WhenAlreadyExistCustomer_InvalidOperationException_ShouldBeReturn",
                Surname = "WhenAlreadyExistCustomer_InvalidOperationException_ShouldBeReturn"
            };
            _Context.Customers.Add(customer);
            _Context.SaveChanges();

            CreateCustomerCommand command = new CreateCustomerCommand(_Context, _Mapper);
            command.Model = new CreateCustomerModel() { Name = customer.Name, Surname = customer.Surname };

            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Eklemek istediğiniz müşteri zaten var");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Customer_ShouldBeCreated()
        {
            CreateCustomerCommand command = new CreateCustomerCommand(_Context, _Mapper);
            CreateCustomerModel model = new CreateCustomerModel()
            {
                Name = "ForHappyCodeCustomer",
                Surname = "ForHappyCodeCustomer"
            };
            command.Model = model;

            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();

            var customer = _Context.Customers.SingleOrDefault(c => c.Name == model.Name && c.Surname == model.Surname);
            customer.Should().NotBeNull();
        }

    }
}


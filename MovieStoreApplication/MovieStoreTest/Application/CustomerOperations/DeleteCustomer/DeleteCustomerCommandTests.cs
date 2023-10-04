using System;
using FluentAssertions;
using MovieStoreApi.Application.CustomerOperations.DeleteCustomer;
using MovieStoreApi.DBOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.CustomerOperations.DeleteCustomer
{
    public class DeleteCustomerCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _Context;
        public DeleteCustomerCommandTests(CommonTestFixture testFixture)
        {
            _Context = testFixture.Context;
        }
        public void WhenTheCustomerIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            var customer = new Entities.Customer
            {
                Name = "WhenTheCustomerIsNotAvailable_InvalidOperationException_ShouldBeReturn",
                Surname = "WhenTheCustomerIsNotAvailable_InvalidOperationException_ShouldBeReturn"
            };
            _Context.Customers.Add(customer);
            _Context.SaveChanges();

            DeleteCustomerCommand command = new DeleteCustomerCommand(_Context);
            command.CustomerId = customer.Id;

            _Context.Remove(customer);
            _Context.SaveChanges();

            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silmek istediğiniz aktör mevcut değil");
        }
        public void WhenValidInputsAreGiven_DeleteCustomer_ShouldNotBeReturnError()
        {
            var customer = new Entities.Customer
            {
                Name = "ForHappyCodeCustomer",
                Surname = "ForHappyCodeCustomer"
            };
            _Context.Customers.Add(customer);
            _Context.SaveChanges();

            DeleteCustomerCommand command = new DeleteCustomerCommand(_Context);
            command.CustomerId = customer.Id;

            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();
        }
    }
}


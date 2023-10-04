using System;
using MovieStoreApi.DBOperations;

namespace MovieStoreApi.Application.CustomerOperations.DeleteCustomer
{
    public class DeleteCustomerCommand
    {
        public int CustomerId { get; set; }
        private readonly MovieStoreDbContext _Context;
        public DeleteCustomerCommand(MovieStoreDbContext Context)
        {
            _Context = Context;
        }
        public async Task Handle()
        {
            var customer = _Context.Customers.FirstOrDefault(c => c.Id == CustomerId);
            if (customer == null)
                throw new InvalidOperationException("Silmek istediğiniz müşteri bulunamadı");

            _Context.Customers.Remove(customer);
            await _Context.SaveChangesAsync();
        }
    }
}


using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreApi.DBOperations;
using MovieStoreApi.Entities;

namespace MovieStoreApi.Application.CustomerOperations.CreateCustomer
{
    public class CreateCustomerCommand
    {
        public CreateCustomerModel Model { get; set; }
        private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;
        public CreateCustomerCommand(MovieStoreDbContext Context, IMapper Mapper)
        {
            _Context = Context;
            _Mapper = Mapper;
        }

        public async Task Handle()
        {
            var customer = _Context.Customers.FirstOrDefault(c => c.Name == Model.Name && c.Surname == Model.Surname);
            if (customer is not null)
                throw new InvalidOperationException("Eklemek istediğiniz müşteri zaten var");

            customer = _Mapper.Map<Customer>(Model);
            _Context.Customers.Add(customer);
            _Context.SaveChanges();
        }
    }

    public class CreateCustomerModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}


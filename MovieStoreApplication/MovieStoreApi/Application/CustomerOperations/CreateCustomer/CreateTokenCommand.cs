using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreApi.DBOperations;
using MovieStoreApi.TokenOperations.Models;

namespace MovieStoreApi.Application.CustomerOperations.CreateCustomer
{
	public class CreateTokenCommand
	{
        public CreateTokenModel Model { get; set; }
        private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;
        private readonly IConfiguration _configuration;
        public CreateTokenCommand(MovieStoreDbContext Context, IMapper Mapper, IConfiguration configuration)
        {
            _Context = Context;
            _Mapper = Mapper;
            _configuration = configuration;
        }
        public async Task<Token> Handle()
		{
            var customer = _Context.Customers.FirstOrDefault(c => c.Email == Model.Email && c.Password == Model.Password);
            if (customer is not null)
            {
                //Token yarat
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(customer);

                customer.RefreshToken = token.RefreshToken;
                customer.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _Context.SaveChanges();
                return token;
            }
            else
                throw new InvalidOperationException("Kullanıcı Adı - Şifre hatalı");
        }
        public class CreateTokenModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}


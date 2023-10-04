using System;
using Microsoft.EntityFrameworkCore;
using MovieStoreApi.DBOperations;
using MovieStoreApi.TokenOperations.Models;

namespace MovieStoreApi.Application.CustomerOperations.CreateCustomer
{
	public class RefreshTokenCommand
	{
        public string RefreshToken { get; set; }
        private readonly IMovieStoreDbContext _Context;
        private readonly IConfiguration _configuration;

        public RefreshTokenCommand(IMovieStoreDbContext Context, IConfiguration configuration)
        {
            _Context = Context;
            _configuration = configuration;
        }
        public async Task<Token> Handle()
		{
            var customer = _Context.Customers.FirstOrDefault(c => c.RefreshToken == RefreshToken && c.RefreshTokenExpireDate > DateTime.Now);
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
	}
}


using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.UnitTest.TestSetup
{
    public class CommonTextFixture
    {
        public BookStoreDbContext context { get; set; }
        public IMapper mapper { get; set; }
        public CommonTextFixture()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(databaseName: "BookStoreTestDB").Options;
            context = new BookStoreDbContext(options);
            context.Database.EnsureCreated();
            context.AddBooks();
            context.AddGenres();
            context.AddAuthors();
            context.SaveChanges();

            mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();
        }
    }
}

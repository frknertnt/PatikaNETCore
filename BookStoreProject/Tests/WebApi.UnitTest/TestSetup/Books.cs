using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTest.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
                   new Book
                   {
                       //Id = 1,
                       Title = "Lean Startup",
                       GenreId = 1,
                       AuthorId = 1,
                       PageCount = 200,
                       PublishDate = new DateTime(2001, 02, 06)
                   },
                   new Book
                   {
                       //Id = 2,
                       Title = "Herland",
                       GenreId = 2,
                       AuthorId = 3,
                       PageCount = 250,
                       PublishDate = new DateTime(2010, 03, 12)
                   },
                   new Book
                   {
                       //Id = 3,
                       Title = "Dune",
                       GenreId = 2,
                       AuthorId = 2,
                       PageCount = 250,
                       PublishDate = new DateTime(2011, 12, 12)
                   });
        }
    }
}

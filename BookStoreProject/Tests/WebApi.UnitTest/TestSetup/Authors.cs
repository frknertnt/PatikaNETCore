using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTest.TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
                    new Author
                    {
                        FirstName = "George",
                        LastName = "Martin",
                        BirthDate = new DateTime(1964, 11, 19)
                    },
                    new Author
                    {
                        FirstName = "Jack",
                        LastName = "London",
                        BirthDate = new DateTime(1974, 05, 24)
                    },
                    new Author
                    {
                        FirstName = "Brad",
                        LastName = "Pitt",
                        BirthDate = new DateTime(1973, 11, 19)
                    }

                    );
        }
    }
}

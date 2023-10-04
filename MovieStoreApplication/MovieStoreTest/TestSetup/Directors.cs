using MovieStoreApi.DBOperations;
using MovieStoreApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreTest.TestSetup
{
    public static class Directors
    {
        public static void AddDirectors(this MovieStoreDbContext Context) 
        {
            Context.Directors.AddRange(
                new Director
                {
                    Name = "David",
                    Surname = "Fincher",
                    Id = 1
                },
                    new Director
                    {
                        Name = "Christopher",
                        Surname = "Nolan",
                        Id = 2
                    },
                    new Director
                    {
                        Name = "Quentin",
                        Surname = "Tarantino",
                        Id = 3
                    }
                );
            
        }

    }
}

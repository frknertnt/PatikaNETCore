using MovieStoreApi.DBOperations;
using MovieStoreApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreTest.TestSetup;

public static class Genres
{
    public static void AddGenres(this MovieStoreDbContext Context)
    {
        Context.Genres.AddRange(

                    new Genre
                    {
                        Name = "Action"
                    },
                    new Genre
                    {
                        Name = "Science fiction"
                    },
                    new Genre
                    {
                        Name = "Drama"
                    }
                    );
        
    }
}

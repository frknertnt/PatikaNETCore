using MovieStoreApi.DBOperations;
using MovieStoreApi.Entities;

namespace MovieStoreTest.TestSetup;

public static class Movies
{
    public static void AddMovies(this MovieStoreDbContext Context)
    {
        Context.Movies.AddRange(

                    new Movie
                    {
                        Name = "Mission Impossible",
                        Year = 1999,
                        Actors = Context.Actors.Where(c => new[] { 1, 3 }.Contains(c.Id)).ToList(),
                        DirectorId = 1,
                        GenreId = 1,
                        Price = 40
                    },
                    new Movie
                    {
                        Name = "Interstellar",
                        Year = 2014,
                        Actors = Context.Actors.Where(c => new[] { 2 }.Contains(c.Id)).ToList(),
                        DirectorId = 2,
                        GenreId = 2,
                        Price = 60
                    },
                    new Movie
                    {
                        Name = "A Man Call Otto",
                        Year = 1999,
                        Actors = Context.Actors.Where(c => new[] { 1, 2, 3 }.Contains(c.Id)).ToList(),
                        DirectorId = 3,
                        GenreId = 3,
                        Price = 80
                    }
                    );
        
    }
}

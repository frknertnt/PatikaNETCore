using Microsoft.EntityFrameworkCore;
using MovieStoreApi.Entities;

namespace MovieStoreApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var Context = new MovieStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<MovieStoreDbContext>>()))
            {
                if (Context.Movies.Any())
                    return;

                Context.Actors.AddRange(

                    new Actor
                    {
                        Name="Leonardo",
                        Surname="Di Caprio",
                        Id=1
                    },
                    new Actor
                    {
                        Name = "John",
                        Surname = "Travolta",
                        Id = 2
                    },
                    new Actor
                    {
                        Name = "Tom",
                        Surname = "Cruise",
                        Id = 3
                    }
                    );
                Context.Directors.AddRange(

                    new Director
                    {
                        Name= "David",
                        Surname= "Fincher",
                        Id=1
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
                Context.Genres.AddRange(

                    new Genre
                    {
                        Name= "Action"
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
                Context.SaveChanges();
                Context.Movies.AddRange(

                    new Movie
                    {
                        Name="Mission Impossible",
                        Year= 1999,
                        Actors= Context.Actors.Where(c=> new[] {1,3}.Contains(c.Id)).ToList(),
                        DirectorId=1,
                        GenreId=1,
                        Price=40
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
                Context.SaveChanges();
            }
        }
    }
}


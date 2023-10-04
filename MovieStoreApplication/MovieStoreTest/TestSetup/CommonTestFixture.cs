using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreApi.Common;
using MovieStoreApi.DBOperations;
using MovieStoreTest.TestSetup;

namespace MovieStoreApi.UnitTests.TestSetup;

public class CommonTestFixture
{
    public MovieStoreDbContext Context { get; set; }
    public IMapper Mapper { get; set; }
    public CommonTestFixture()
    {
        var options = new DbContextOptionsBuilder<MovieStoreDbContext>().UseInMemoryDatabase(databaseName: "MvStrTestDb").Options;
        Context = new MovieStoreDbContext(options);

        Context.Database.EnsureCreated();
        //Context.AddActors();
        //Context.AddMovies();
        //Context.AddDirectors();
        //Context.AddGenres();
        //Context.SaveChanges();

        Mapper = new MapperConfiguration(c => { c.AddProfile<MappingProfile>(); }).CreateMapper();
    }
}

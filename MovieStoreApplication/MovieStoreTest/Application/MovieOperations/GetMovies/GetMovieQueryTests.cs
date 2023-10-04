using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.MovieOperations.GetMovies;
using MovieStoreApi.DBOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.MovieOperations.GetMovies
{
    public class GetMovieQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;
        public GetMovieQueryTests(CommonTestFixture testFixture)
        {
            _Context = testFixture.Context;
            _Mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenQueryGetResult_Movie_ShouldNotBeReturnErrors()
        {
            GetMovieQuery query = new GetMovieQuery(_Context, _Mapper);
            FluentActions
                .Invoking(() => query.Handle().GetAwaiter().GetResult()).Invoke();
        }
    }
}


using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.ActorOperations.GetActors;
using MovieStoreApi.DBOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.ActorOperations.GetActors
{
    public class GetActorQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;
        public GetActorQueryTests(CommonTestFixture testFixture)
        {
            _Context = testFixture.Context;
            _Mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenQueryGetResult_Movie_ShouldNotBeReturnErrors()
        {
            GetActorQuery query = new GetActorQuery(_Context, _Mapper);
            FluentActions.Invoking(()=> query.Handle().GetAwaiter().GetResult());
        }
    }
}


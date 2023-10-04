using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.DirectorOperations.GetDirectors;
using MovieStoreApi.DBOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.DirectorOperations.GetDirectors
{
    public class GetDirectorQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;
        public GetDirectorQueryTests(CommonTestFixture testFixture)
        {
            _Context = testFixture.Context;
            _Mapper = testFixture.Mapper;
        }
        public void WhenQueryGetResult_Director_ShouldNotBeReturnErrors()
        {
            GetDirectorQuery query = new GetDirectorQuery(_Context, _Mapper);
            FluentActions
                .Invoking(() => query.Handle().GetAwaiter().GetResult()).Invoke();
        }
    }
}


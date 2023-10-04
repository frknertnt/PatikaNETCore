using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.DirectorOperations.GetDirectorDetail;
using MovieStoreApi.DBOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.DirectorOperations.GetDirectorDetail
{
    public class DirectorDetailQueryValidatorTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;
        public DirectorDetailQueryValidatorTests(CommonTestFixture testFixture)
        {
            _Context = testFixture.Context;
            _Mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenDirectorIdLessThanZero_Validator_ShouldBeReturnError()
        {
            DirectorDetailQuery query = new DirectorDetailQuery(_Context, _Mapper);
            query.DirectorId = 0;

            DirectorDetailQueryValidator validator = new DirectorDetailQueryValidator();
            var result = validator.Validate(query);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {
            var director = new Entities.Director
            {
                Name = "ForHappyCode",
                Surname = "ForHappyCode"
            };
            _Context.Directors.Add(director);
            _Context.SaveChanges();

            var directorId = director.Id;

            DirectorDetailQuery query = new DirectorDetailQuery(_Context, _Mapper);
            query.DirectorId = directorId;

            DirectorDetailQueryValidator validator = new DirectorDetailQueryValidator();
            var result = validator.Validate(query);
            result.Errors.Count.Should().Be(0);
        }
    }
}


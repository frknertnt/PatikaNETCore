using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.ActorOperations.GetActorDetail;
using MovieStoreApi.DBOperations;
using MovieStoreApi.Entities;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.ActorOperations.GetActorDetail
{
    public class GetActorDetailQueryValidatorTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;
        public GetActorDetailQueryValidatorTests(CommonTestFixture testFixture)
        {
            _Context = testFixture.Context;
            _Mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenActorIdLessThanZero_Validator_ShouldBeReturnError()
        {
            ActorDetailQuery query = new ActorDetailQuery(_Context, _Mapper);
            query.ActorId = 0;

            ActorDetailQueryValidator validator = new ActorDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {
            var actor = new Actor()
            {
                Name = "ForHappyCode",
                Surname = "ForHappyCode"
            };
            _Context.Actors.Add(actor);
            _Context.SaveChanges();

            ActorDetailQuery query = new ActorDetailQuery(_Context, _Mapper);
            query.ActorId = actor.Id;

            ActorDetailQueryValidator validator = new ActorDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().Be(0);

        }
    }
}


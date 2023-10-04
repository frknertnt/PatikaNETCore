using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.ActorOperations.GetActorDetail;
using MovieStoreApi.DBOperations;
using MovieStoreApi.Entities;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.ActorOperations.GetActorDetail
{
    public class GetActorDetailQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;
        public GetActorDetailQueryTests(CommonTestFixture testFixture)
        {
            _Context = testFixture.Context;
            _Mapper = testFixture.Mapper;
        }
        [Fact]
        //Arrange
        public void WhenTheActorIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            var actor = new Actor()
            {
                Name = "ForNoExistActor",
                Surname = "ForNoExistActor"
            };
            _Context.Actors.Add(actor);
            _Context.SaveChanges();

            var actorId = actor.Id;
            _Context.Actors.Remove(actor);
            _Context.SaveChanges();

            ActorDetailQuery query = new ActorDetailQuery(_Context, _Mapper);
            query.ActorId = actorId;

            //Act
            FluentActions
                .Invoking(() => query.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aktör mevcut değil");
        }
        [Fact]
        public void WhenTheActorIsNotAvailable_Actor_ShouldNotBeReturnErrors()
        {
            var actor = new Actor
            {
                Name = "ForHappyCode",
                Surname = "ForHapyCode"
            };
            _Context.Actors.Add(actor);
            _Context.SaveChanges();

            ActorDetailQuery query = new ActorDetailQuery(_Context, _Mapper);
            query.ActorId = actor.Id;

            FluentActions
                .Invoking(() => query.Handle().GetAwaiter().GetResult()).Invoke();
        }
    }
}


using System;
using FluentAssertions;
using MovieStoreApi.Application.ActorOperations.DeleteActor;
using MovieStoreApi.DBOperations;
using MovieStoreApi.Entities;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.ActorOperations.DeleteActor
{
    public class DeleteActorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _Context;
        public DeleteActorCommandTests(CommonTestFixture testFixture)
        {
            _Context = testFixture.Context;
        }
        [Fact]
        public void WhenTheActorIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            var actor = new Actor()
            {
                Name = "ForNotExistActor",
                Surname = "ForNotExistActor"
            };
           _Context.Actors.Add(actor);
            _Context.SaveChanges();

            var actorId = actor.Id;
            _Context.Actors.Remove(actor);
            _Context.SaveChanges();
            DeleteActorCommand command = new DeleteActorCommand(_Context);
            command.ActorId = actorId;

            //Act
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silmek istediğiniz aktör mevcut değil");
        }
        [Fact]
        public void WhenValidInputsAreGiven_DeleteActor_ShouldNotBeReturnError()
        {
            var actor = new Actor()
            {
              Name="ForHappyCode",
              Surname="ForHappyCode"
            };
            _Context.Actors.Add(actor);
            _Context.SaveChanges();
            DeleteActorCommand command = new DeleteActorCommand(_Context);
            command.ActorId = actor.Id;
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();
        }
    }
}


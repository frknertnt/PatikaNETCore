using System;
using System.Numerics;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.ActorOperations.CreateActor;
using MovieStoreApi.Application.DirectorOperations.CreateDirector;
using MovieStoreApi.DBOperations;
using MovieStoreApi.Entities;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.ActorOperations.CreateActor
{
    public class CreateActorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;

        public CreateActorCommandTests(CommonTestFixture testFixture)
        {
            _Context = testFixture.Context;
            _Mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistActor_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            var actor = new Actor
            {
                Name = "WhenAlreadyExistActorNameIsGiven_InvalidOperationException_ShouldBeReturn",
                Surname = "WhenAlreadyExistActorSurnameIsGiven_InvalidOperationException_ShouldBeReturn"
            };
            _Context.Actors.Add(actor);
            _Context.SaveChanges();

            CreateActorCommand command = new (_Context, _Mapper);
            command.Model = new CreateActorModel() { Name = actor.Name, Surname = actor.Surname };

            //Act & Assert (Çalıştırma-Doğrulama)
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Eklemek istediğiniz aktör zaten var");

        }
        [Fact]
        public void WhenValidInputsAreGiven_Actor_ShouldBeCreated()
        {
            //Arrange
            CreateActorCommand command = new CreateActorCommand(_Context, _Mapper);
            CreateActorModel model = new CreateActorModel()
            {
                Name = "WhenValidInputsAreGiven_Actor_ShouldBeCreated",
                Surname = "WhenValidInputsAreGiven_Actor_ShouldBeCreated"
            };
            command.Model = model;

            //Act
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();

            //Assert
            var director = _Context.Actors.SingleOrDefault(c => c.Name == model.Name && c.Surname == model.Surname);
            director.Should().NotBeNull();
        }

    }
}


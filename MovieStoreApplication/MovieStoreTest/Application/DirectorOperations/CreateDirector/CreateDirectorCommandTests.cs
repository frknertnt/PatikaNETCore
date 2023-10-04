using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreApi.Application.DirectorOperations.CreateDirector;
using MovieStoreApi.DBOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.DirectorOperations.CreateDirector
{
    public class CreateDirectorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;
        public CreateDirectorCommandTests(CommonTestFixture testFixture)
        {
            _Context = testFixture.Context;
            _Mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistDirector_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            var director = new Entities.Director()
            {
                Name = "WhenAlreadyExistDirector_InvalidOperationException_ShouldBeReturn",
                Surname = "WhenAlreadyExistDirector_InvalidOperationException_ShouldBeReturn"
            };
            _Context.Directors.Add(director);
            _Context.SaveChanges();

            CreateDirectorCommand command = new CreateDirectorCommand(_Context, _Mapper);
            command.Model = new CreateDirectorModel() { Name = director.Name, Surname = director.Surname };

            //Act
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Eklemek istediğiniz yönetmen mevcut");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Director_ShouldBeCreated()
        {
            //Arrange
            CreateDirectorCommand command = new CreateDirectorCommand(_Context, _Mapper);
            CreateDirectorModel model = new CreateDirectorModel()
            {
                Name = "WhenValidInputsAreGiven_Director_ShouldBeCreated",
                Surname = "WhenValidInputsAreGiven_Director_ShouldBeCreated"
            };
            command.Model = model;

            //Act
            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();

            //Assert
            var director = _Context.Directors.SingleOrDefault(c => c.Name == model.Name && c.Surname == model.Surname);
            director.Should().NotBeNull();
        }
    }
}


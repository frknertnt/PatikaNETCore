using System;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieStoreApi.Application.ActorOperations.CreateActor;
using MovieStoreApi.DBOperations;
using MovieStoreApi.UnitTests.TestSetup;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MovieStoreApi.UnitTests.Application.ActorOperations.CreateActor
{
    public class CreateActorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _Context;
        private readonly IMapper _Mapper;

        public CreateActorCommandValidatorTests(CommonTestFixture testFixture)
        {
            _Context = testFixture.Context;
            _Mapper = testFixture.Mapper;
        }

        [Theory]
        [InlineData("", "surname")]
        [InlineData("name", "")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturn(string name,string surname)
        {
           
            //Arrange
            CreateActorCommand command = new (null, null);
            command.Model = new CreateActorModel
            {
                Name = name,
                Surname = surname
            };
            //Act
            CreateActorCommandValidator validator = new ();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {
            CreateActorCommand command = new (null, null);
            command.Model = new CreateActorModel
            {
                Name = "cansel",
                Surname = "Hizmet"
            };
            CreateActorCommandValidator validator = new ();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}


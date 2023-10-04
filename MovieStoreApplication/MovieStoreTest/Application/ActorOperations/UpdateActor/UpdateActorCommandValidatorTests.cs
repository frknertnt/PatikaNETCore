﻿using System;
using FluentAssertions;
using MovieStoreApi.Application.ActorOperations.UpdateActor;
using MovieStoreApi.DBOperations;
using MovieStoreApi.Entities;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.ActorOperations.UpdateActor
{
    public class UpdateActorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _Context;
        public UpdateActorCommandValidatorTests(CommonTestFixture testFixture)
        {
            _Context = testFixture.Context;
        }
        [Theory]
        [InlineData(1, "", "surname")]
        [InlineData(1, "name", "")]
        [InlineData(0, "name", "surname")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int id, string name, string surname)
        {
            //Arrange
            UpdateActorCommand command = new UpdateActorCommand(null);
            command.Model = new UpdateActorModel()
            {
                Name = name,
                Surname = surname
            };
            command.ActorId = id;
            UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);

        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {
            //Arrange
            UpdateActorCommand command = new UpdateActorCommand(_Context);
            var actor = new Actor()
            {
                Name = "ForHappyCode",
                Surname = "ForHappyCode"
            };
            _Context.Actors.Add(actor);
            _Context.SaveChanges();

            UpdateActorModel model = new UpdateActorModel();
            command.Model = new UpdateActorModel()
            {
                Name = "ForHappyCodeTest",
                Surname = "ForHappyCodeTest"
            };
            command.ActorId = actor.Id;
            UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }

}

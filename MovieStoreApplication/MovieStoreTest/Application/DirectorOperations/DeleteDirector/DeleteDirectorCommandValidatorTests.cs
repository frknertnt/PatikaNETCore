using System;
using FluentAssertions;
using MovieStoreApi.Application.DirectorOperations.DeleteDirector;
using MovieStoreApi.DBOperations;
using MovieStoreApi.UnitTests.TestSetup;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MovieStoreApi.UnitTests.Application.DirectorOperations.DeleteDirector
{
    public class DeleteDirectorCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _Context;
        public DeleteDirectorCommandValidatorTests(CommonTestFixture testFixture)
        {
            _Context = testFixture.Context;
        }
        [Fact]
        public void WhenDirectorIdLessThanZero_Validator_ShouldBeReturnError()
        {
            DeleteDirectorCommand command = new DeleteDirectorCommand(_Context);
            command.DirectorId = 0;

            DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {
            var director = new Entities.Director
            {
                Name = "ForHappyCodeDirectorValidator",
                Surname = "ForHappyCodeDirectorValidator"
            };
            _Context.Directors.Add(director);
            _Context.SaveChanges();

            var directorId = director.Id;

            _Context.Remove(director);
            _Context.SaveChanges();

            DeleteDirectorCommand command = new DeleteDirectorCommand(_Context);
            command.DirectorId = directorId;

            DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}


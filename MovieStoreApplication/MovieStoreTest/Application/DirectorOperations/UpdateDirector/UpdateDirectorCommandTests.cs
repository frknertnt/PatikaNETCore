using System;
using FluentAssertions;
using MovieStoreApi.Application.DirectorOperations.UpdateDirector;
using MovieStoreApi.DBOperations;
using MovieStoreApi.UnitTests.TestSetup;

namespace MovieStoreApi.UnitTests.Application.DirectorOperations.UpdateDirector
{
    public class UpdateDirectorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _Context;
        public UpdateDirectorCommandTests(CommonTestFixture testFixture)
        {
            _Context = testFixture.Context;
        }
        [Fact]
        public void WhenTheDirectorIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            var director = new Entities.Director
            {
                Name = "WhenTheDirectorIsNotAvailable_InvalidOperationException_ShouldBeReturn",
                Surname = "WhenTheDirectorIsNotAvailable_InvalidOperationException_ShouldBeReturn"
            };
            _Context.Directors.Add(director);
            _Context.SaveChanges();

            var directorId = director.Id;

            _Context.Directors.Remove(director);
            _Context.SaveChanges();

            UpdateDirectorCommand command = new UpdateDirectorCommand(_Context);
            command.DirectorId = director.Id;

            FluentActions
                .Invoking(() => command.Handle().GetAwaiter().GetResult())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yönetmen bulunamadı");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Director_ShouldBeUpdated()
        {
            var director = new Entities.Director
            {
                Name = "updateTest",
                Surname = "updateTest"
            };
            _Context.Directors.Add(director);
            _Context.SaveChanges();

            UpdateDirectorCommand command = new UpdateDirectorCommand(_Context);

            UpdateDirectorModel model = new UpdateDirectorModel()
            {
                Name = "ForHappyCode",
                Surname = "ForHappyTest"
            };
            command.DirectorId = director.Id;
            command.Model = model;
            FluentActions
                .Invoking(() => command.Handle()).Invoke();
            director=_Context.Directors.FirstOrDefault(c=> c.Id== command.DirectorId);
            director.Name.Should().Be(model.Name);
            director.Surname.Should().Be(model.Surname);

        }
    }
}


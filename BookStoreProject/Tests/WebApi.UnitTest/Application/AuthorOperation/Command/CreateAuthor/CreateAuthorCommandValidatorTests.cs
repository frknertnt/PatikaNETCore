using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;
using WebApi.UnitTest.TestSetup;
using Xunit;

namespace WebApi.UnitTest.Application.AuthorOperation.Command.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTextFixture>
    {
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;

        public CreateAuthorCommandValidatorTests(CommonTextFixture textFixture)
        {
            context = textFixture.context;
            mapper = textFixture.mapper;
        }

        [Theory]
        [InlineData(" ", " ")]
        [InlineData(" ", "qwe")]
        [InlineData("qwe", " ")]
        [InlineData("qw", "w")]
        [InlineData("q", "ea")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string firstname, string lastname)
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null!, null!);
            command.Model = new CreateAuthorModel() 
            { 
                FirstName = firstname, 
                LastName = lastname, 
                BirthDate = new DateTime(1917, 11, 22) 
            };

            //act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null!, null!);
            command.Model = new CreateAuthorModel()
            {
                FirstName = "Jeff",
                LastName = "Hardy",
                BirthDate = DateTime.Now.Date

            };

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Theory]
        [InlineData("jpod ", " fgjk")]
        [InlineData("hgdb", "hkjd")]
        [InlineData("sa  ", "da  ")]
        [InlineData(" de ", " e  ")]
        [InlineData("qwtertyqqwe", "qwetwqeq")]
        [InlineData(" ddd", "baa ")]
        public void WhenValidInputAreGiven_Validator_ShouldBeReturnErrors(string firstname, string lastname)
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null!, null!);
            command.Model = new CreateAuthorModel() 
            { 
                FirstName = firstname, 
                LastName = lastname, 
                BirthDate = new DateTime(1917, 05, 22) 
            };

            //act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);

        }
    }
}

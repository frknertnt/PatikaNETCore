using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DBOperations;
using WebApi.UnitTest.TestSetup;
using Xunit;

namespace WebApi.UnitTest.Application.BookOperation.Queries
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTextFixture>
    {
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;

        public GetBookDetailQueryTests(CommonTextFixture testFixture)
        {
            context = testFixture.context;
            mapper = testFixture.mapper;
        }

        [Fact]
        public void WhenGivenBookIdIsInNotDataBase_InvalidOperationException_ShouldBeReturn()
        {
            GetBookDetailQuery command = new GetBookDetailQuery(context, mapper);
            command.BookId = 0;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should()
                .Be("Book not found");
        }

        [Fact]
        public void WhenGivenBookIdIsInDataBase_InvalidOperationException_ShouldBeReturn()
        {
            GetBookDetailQuery command = new GetBookDetailQuery(context, mapper);
            command.BookId = 1;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var book = context.Books.SingleOrDefault(b=>b.Id == command.BookId);
            book.Should().NotBeNull(); // LINQ ile dönen book nesnesinin boş olmamalı 
        }
    }
}

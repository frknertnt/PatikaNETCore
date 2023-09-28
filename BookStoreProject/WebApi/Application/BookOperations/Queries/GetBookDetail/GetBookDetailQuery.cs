using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId { get; set; }
        public GetBookDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books
                .Include(x=>x.Genre)
                .Include(x=>x.Author)
                .Where(b => b.Id == BookId).SingleOrDefault();
            if (book is null)
            {
                throw new InvalidOperationException("Book not found");
            }
            BookDetailViewModel viewModel = _mapper.Map<BookDetailViewModel>(book);
            //viewModel.Title = book.Title;
            //viewModel.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
            //viewModel.Genre = ((GenreEnum)book.GenreId).ToString();
            return viewModel;
        }

    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}

using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly IBookStoreDbContext _context;
        public int Id { get; set; }
        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == Id);

            if (author is null)
                throw new InvalidOperationException("Author not found.");

            var isHasAnyBook = _context.Books.FirstOrDefault(x => x.AuthorId == author.Id);

            if (isHasAnyBook is not null)
                throw new InvalidOperationException("The transaction cannot be completed while there is a book belonging to the author.");

            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}
using my_bookAPI.Data.Model;
using my_bookAPI.Data.ViewModels;

namespace my_bookAPI.Data.Services
{
    public class AuthorsService
    {
        private AppDbContext _context;
        public AuthorsService(AppDbContext context)
        {
            _context = context;
        }

        public void AddAuthor(AuthorVM book)
        {
            var _author = new Author()
            {
               FullName = book.FullName,
            };
            _context.Authors.Add(_author);
            _context.SaveChanges();
        }

        public AuthorWithBooksVM GetAuthorWithBooks(int authorId)
        {
            var _author = _context.Authors.Where(n => n.Id == authorId).Select(n => new AuthorWithBooksVM()
            {
                FullName = n.FullName,
                BookTitles = n.Book_Author.Select(n => n.Book.Title).ToList()
            }).FirstOrDefault();

            return _author;
        }

        public void DeleteAuthorById(int id)
        {
            var _author = _context.Authors.FirstOrDefault(n => n.Id == id);

            if (_author != null)
            {
                _context.Authors.Remove(_author);
                _context.SaveChanges();
            }
        }

    }
}

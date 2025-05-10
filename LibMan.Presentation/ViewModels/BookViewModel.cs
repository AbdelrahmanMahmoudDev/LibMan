using LibMan.Domains;

namespace LibMan.Presentation.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public List<Book> AllBooks { get; set; } = new List<Book>();

        public BookViewModel() { }
        public BookViewModel(Book book)
        {
            if (book is not null)
                Id = book.Id;
        }
    }
}
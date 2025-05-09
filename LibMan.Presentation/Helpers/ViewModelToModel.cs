using LibMan.Domains;
using LibMan.Presentation.Areas.Admin.ViewModels;

namespace LibMan.Presentation.Helpers
{
    public static class ViewModelToModel
    {
        public static Author AuthorViewModelToModel(AuthorViewModel authorViewModel)
        {
            if(authorViewModel == null)
                throw new ArgumentNullException();

            Author newAuthor = new Author()
            {
                Id = authorViewModel.Id,
                FullName = authorViewModel.FullName,
                Email = authorViewModel.Email,
                Website = authorViewModel.Website,
                Bio = authorViewModel.Bio
            };

            return newAuthor;
        }

        public static Book BookViewModelToModel(BookViewModel bookViewModel)
        {
            if (bookViewModel == null)
                throw new ArgumentNullException();

            Book newBook = new Book()
            {
                Id = bookViewModel.Id,
                Title = bookViewModel.Title,    
                Genre = (GenreType)bookViewModel.Genre,
                Description = bookViewModel.Description,
                IsAvailable = bookViewModel.IsAvailable,
                AuthorId = bookViewModel.AuthorId,
            };

            return newBook;
        }
    }
}

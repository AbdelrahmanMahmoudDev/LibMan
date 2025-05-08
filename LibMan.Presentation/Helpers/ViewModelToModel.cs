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
    }
}

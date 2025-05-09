using System.ComponentModel.DataAnnotations;
using LibMan.Domains;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LibMan.Presentation.Areas.Admin.ViewModels
{
    public class BookViewModel
    {
        public const int DescriptionMax = 300;
        public int Id { get; set; }
        [Required(ErrorMessage = "* This field is required")]
        public string Title { get; set; } = null!;
        [Required(ErrorMessage = "* This field is required")]
        public GenreType? Genre { get; set; }
        [MaxLength(DescriptionMax, ErrorMessage = $"The description cannot exceed the maximium letter count")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "* This field is required")]
        public bool IsAvailable { get; set; }
        [Required(ErrorMessage = "* This field is required")]
        public int AuthorId { get; set; }
        [ValidateNever]
        public string AuthorName { get; set; } = null!;
        public List<Author> AllAuthors { get; set; } = new List<Author>();
        public static List<GenreType> GenreList { get; set; } =
        Enum.GetValues(typeof(GenreType)).Cast<GenreType>().ToList();

        public BookViewModel() { }
        public BookViewModel(Book book)
        {
            if (book is not null)
            {
                Id = book.Id;
                AuthorId = book.AuthorId;
                Title = book.Title;
                Genre = book.Genre;
                Description = book.Description;
                IsAvailable = book.IsAvailable;
                AuthorName = book.Author?.FullName ?? string.Empty;
            }
        }
    }
}

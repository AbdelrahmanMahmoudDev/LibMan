using System.ComponentModel.DataAnnotations;
using LibMan.Domains;

namespace LibMan.Presentation.Areas.Admin.ViewModels
{
    public class AuthorViewModel
    {
        public const int BioMax = 300;
        public int Id { get; set; }
        [Required(ErrorMessage = "*")]
        public string FullName { get; set; } = string.Empty;
        [MaxLength(100, ErrorMessage = "The email cannot exceed 100 characters")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [Required(ErrorMessage = "*")]
        public string Email { get; set; } = string.Empty;
        [MaxLength(BioMax, ErrorMessage = $"The biography cannot exceed the maximium letter count")]
        public string? Website { get; set; }
        public string? Bio { get; set; }

        public AuthorViewModel() { }
        public AuthorViewModel(Author author)
        {
            if (author is not null)
            {
                Id = author.Id;
                FullName = author.FullName;
                Email = author.Email;
                Website = author.Website;
                Bio = author.Bio;
            }
        }
    }
}

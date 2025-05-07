using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMan.Data.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        [MaxLength(100, ErrorMessage="The email cannot exceed 100 characters")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;
        public string? Website { get; set; }
        public string? Bio { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMan.Data.Models
{
    public enum GenreType
    {
        Unknown = 0, Adventure = 1, Mystery = 2, Thriller = 3, Romance = 4, SciFi = 5, Fantasy = 6, Biography = 7, History = 8, SelfHelp = 9, Children = 10, YoungAdult = 11, Poetry = 12, Drama = 13, NonFiction = 14
    }

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public GenreType Genre { get; set; }
        public string? Description { get; set; }
        public bool IsAvailable { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;
        public ICollection<BorrowTransaction> BorrowTransactions { get; set; } = new List<BorrowTransaction>();
    }
}

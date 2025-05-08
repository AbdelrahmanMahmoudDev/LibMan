namespace LibMan.Domains
{
    public class BorrowTransaction
    {
        public int Id { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
    }
}

namespace LibMan.Domains
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Website { get; set; }
        public string? Bio { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}

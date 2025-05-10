using LibMan.Domains;
using Microsoft.EntityFrameworkCore;

namespace LibMan.Data
{
    public class MainContext : DbContext
    {
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BorrowTransaction> BorrowTransactions { get; set; }

        public MainContext() : base() { }
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
            configurationBuilder.Properties<string>().HaveMaxLength(300);
            configurationBuilder.Properties<DateTime>().HaveColumnType("DateTime");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>(e =>
            {
                e.HasKey(p => p.Id);

                e.HasIndex(p => p.FullName)
                 .IsUnique();

                e.HasIndex(p => p.Email)
                 .IsUnique();
            });

            modelBuilder.Entity<Book>(e =>
            {
                e.HasKey(p => p.Id);

                e.Property(p => p.Genre)
                 .HasConversion<int>();

                e.HasOne(p => p.Author)
                 .WithMany(p => p.Books)
                 .HasForeignKey(p => p.AuthorId);
            });

            modelBuilder.Entity<BorrowTransaction>(e =>
            {
                e.HasKey(p => p.Id);

                e.HasOne(p => p.Book)
                 .WithMany(p => p.BorrowTransactions)
                 .HasForeignKey(p => p.BookId);
            });

            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, FullName = "Joanne Kathleen Rowling John", Email = "jkrowling@example.com" },
                new Author { Id = 2, FullName = "George Orwell John Doe", Email = "orwell@example.com" },
                new Author { Id = 3, FullName = "Charles Dickens John Doe", Email = "charles@example.com" }
                );

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Harry Potter", Genre = GenreType.Adventure, AuthorId = 1, IsAvailable = true },
                new Book { Id = 2, Title = "1984", Genre = GenreType.Fantasy, AuthorId = 2, IsAvailable = true },
                new Book { Id = 3, Title = "Animal Farm", Genre = GenreType.Fantasy, AuthorId = 2, IsAvailable = false },
                new Book { Id = 4, Title = "A Tale of Two Cities", Genre = GenreType.Fantasy, AuthorId = 3, IsAvailable = true },
                new Book { Id = 5, Title = "Oliver Twist", Genre = GenreType.Fantasy, AuthorId = 3, IsAvailable = true },
                new Book { Id = 6, Title = "A Christmas Carol", Genre = GenreType.Fantasy, AuthorId = 3, IsAvailable = true }
                );

            modelBuilder.Entity<BorrowTransaction>().HasData(
                new BorrowTransaction { Id = 1, BorrowDate = DateTime.Now, ReturnDate = null, BookId = 3 }
                );
        }
    }
}

using LibMan.Data;
using LibMan.Domains;
using LibMan.Data.Repository;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace LibMan.Tests
{
    public class GenericRepositoryTests
    {
        private readonly Mock<MainContext> _MockContext;
        private readonly Mock<DbSet<Author>> _MockDbSet;
        private readonly GenericRepository<Author> _Repository;

        public GenericRepositoryTests()
        {
            _MockContext = new Mock<MainContext>();
            _MockDbSet = new Mock<DbSet<Author>>();

            _MockContext.Setup(c => c.Set<Author>()).Returns(_MockDbSet.Object);

            _Repository = new GenericRepository<Author>(_MockContext.Object);
        }

        [Fact]
        public async Task AddAsync_OnSuccess_ReturnsTrue()
        {
            // Arrange
            Author testAuthor = new Author();

            // Act
            bool result = await _Repository.AddAsync(testAuthor);

            // Assert
            Assert.True(result);
            _MockContext.Verify(c => c.AddAsync(testAuthor, default), Times.Once);
        }

        [Fact]
        public async Task AddAsync_NullEntity_ThrowsArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _Repository.AddAsync(null));
        }

        [Fact]
        public void Delete_OnSuccess_ReturnsTrue()
        {
            // Arrange
            Author testAuthor = new Author();

            // Act
            bool result = _Repository.Delete(testAuthor);

            // Assert
            Assert.True(result);
            _MockContext.Verify(c => c.Remove(testAuthor), Times.Once);
        }

        [Fact]
        public void Delete_NullEntity_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _Repository.Delete(null));
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllEntities()
        {
            // Arrange
            var data = new List<Book>()
            {
                new Book { Id = 1, Title = "Harry Potter", Genre = GenreType.Adventure, AuthorId = 1, IsAvailable = true },
                new Book { Id = 2, Title = "1984", Genre = GenreType.Fantasy, AuthorId = 2, IsAvailable = true }
            }.BuildMockDbSet();

            _MockContext.Setup(c => c.Set<Book>()).Returns(data.Object);
            var repository = new GenericRepository<Book>(_MockContext.Object);

            // Act
            IEnumerable<Book> books = await repository.GetAllAsync();

            // Assert
            Assert.Equal(2, books.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsEntity()
        {
            // Arrange
            Author testAuthor = new Author { Id = 1, FullName = "John Doe John Doe", Email = "john@test.com" };
            _MockDbSet.Setup(x => x.FindAsync(1)).ReturnsAsync(testAuthor);

            // Act
            Author resultAuthor = await _Repository.GetByIdAsync(1);

            // Assert
            Assert.Equal(testAuthor, resultAuthor);
        }

        [Fact]
        public async Task GetByIdAsync_IdLessThanZero_ThrowsInvalidOperationException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _Repository.GetByIdAsync(-1));
        }

        [Fact]
        public void Update_OnSuccess_ReturnsTrue()
        {
            // Arrange
            Author testAuthor = new Author();

            // Act
            bool result = _Repository.Update(testAuthor);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Update_NullEntity_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _Repository.Update(null));
        }
    }
}

using LibMan.Business.Book.Service;
using LibMan.Data.Repository;

namespace LibMan.Business.Pagination
{
    public class PaginatedBookService : BookService, IPaginatedBookService
    {
        public PaginatedBookService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<PagedResult<Domains.Book>> GetPaginatedBooksAsync(int pageNumber, int pageSize)
        {
            var allBooks = await base.GetAllBooksWithAuthor();

            var pagedBooks = allBooks
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);                  

            var model = new PagedResult<Domains.Book>
            {
                Items = pagedBooks,
                TotalItems = allBooks.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return model;
        }

        public async Task<PagedResult<Domains.Book>> GetPaginatedBooksWithFiltersAsync(List<string> statuses, int pageNumber = 1, int pageSize = 5)
        {
            List<Domains.Book> allBooksList = await GetAllBooksWithAuthor();
            IQueryable<Domains.Book> allBooksQueryable = allBooksList.AsQueryable();


            if (statuses?.Count() > 0)
            {
                List<bool> boolStatuses = new List<bool>();
                foreach (string status in statuses)
                {
                    boolStatuses.Add(bool.Parse(status));
                }

                var isAvailable = boolStatuses.Contains(true);
                var isBorrowed = boolStatuses.Contains(false);

                allBooksQueryable = allBooksQueryable.Where(b => (isAvailable && b.IsAvailable) || (isBorrowed && !b.IsAvailable));

            }

            var paginatedBooks = allBooksQueryable
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var model = new PagedResult<Domains.Book>()
            {
                Items = paginatedBooks,
                TotalItems = allBooksQueryable.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return model;
        }
    }
}

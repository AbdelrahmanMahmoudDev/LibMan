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

        public void PrepareStatusList(List<string> statuses, List<Domains.Book> allBooksList, out IQueryable<Domains.Book> allBooksQueryable)
        {
            allBooksQueryable = allBooksList.AsQueryable();

            if (statuses?.Count() > 0)
            {
                List<bool> boolStatuses = new List<bool>();
                foreach (string status in statuses)
                {
                    boolStatuses.Add(bool.Parse(status));
                }

                bool isAvailable = boolStatuses.Contains(true);
                bool isBorrowed = boolStatuses.Contains(false);

                allBooksQueryable = allBooksQueryable.Where(b => (isAvailable && b.IsAvailable) || (isBorrowed && !b.IsAvailable));

            }
        }

        public PagedResult<Domains.Book> PreparePagedResult(IQueryable<Domains.Book> allBooksQueryable, int pageNumber, int pageSize)
        {
            var paginatedBooks = allBooksQueryable
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            PagedResult<Domains.Book> model = new PagedResult<Domains.Book>()
            {
                Items = paginatedBooks,
                TotalItems = allBooksQueryable.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return model;
        }

        public async Task<PagedResult<Domains.Book>> GetPaginatedBooksWithFiltersAsync(List<string> statuses, int pageNumber = 1, int pageSize = 5)
        {
            List<Domains.Book> allBooksList = await GetAllBooksWithAuthor();

            PrepareStatusList(statuses, allBooksList, out IQueryable<Domains.Book> allBooksQueryable);

            return PreparePagedResult(allBooksQueryable, pageNumber, pageSize);
        }
    }
}

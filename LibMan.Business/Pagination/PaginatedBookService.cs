using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibMan.Business.Book.Service;
using LibMan.Data;
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
    }
}

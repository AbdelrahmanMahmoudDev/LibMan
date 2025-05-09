using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibMan.Business.Book.Service;

namespace LibMan.Business.Pagination
{
    public interface IPaginatedBookService : IBookService
    {
        public Task<PagedResult<Domains.Book>> GetPaginatedBooksAsync(int pageNumber, int pageSize);

        public Task<PagedResult<Domains.Book>> GetPaginatedBooksWithFiltersAsync(List<string> statuses, int pageNumber , int pageSize);
    }
}

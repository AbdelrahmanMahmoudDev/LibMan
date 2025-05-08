
using LibMan.Data.Repository;

namespace LibMan.Business.Author
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _UnitOfWork;

        public AuthorService(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        public async Task<List<Data.Models.Author>> GetAllAuthors()
        {
            IEnumerable<Data.Models.Author> result = await _UnitOfWork.Authors.GetAllAsync();
            return result.ToList();
        }
    }
}

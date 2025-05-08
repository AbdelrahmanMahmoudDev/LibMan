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

        public async Task<List<Domains.Author>> GetAllAuthors()
        {
            IEnumerable<Domains.Author> result = await _UnitOfWork.Authors.GetAllAsync();
            return result.ToList();
        }

        public async Task<Domains.Author> GetAuthorBasedOnId(int id)
        {
            return await _UnitOfWork.Authors.GetByIdAsync(id);
        }

        public async Task<bool> SaveNew(Domains.Author newAuthor)
        {
            try
            {
                await _UnitOfWork.Authors.AddAsync(newAuthor);
                await _UnitOfWork.SaveAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SaveUpdate(Domains.Author newAuthor)
        {
            try
            {
                _UnitOfWork.Authors.Update(newAuthor);
                await _UnitOfWork.SaveAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveAuthor(int id)
        {
            try
            {
                Domains.Author targetAuthor = await _UnitOfWork.Authors.GetByIdAsync(id);

                if (targetAuthor is not null)
                {
                    _UnitOfWork.Authors.Delete(targetAuthor);

                    await _UnitOfWork.SaveAsync();

                    return true;
                }
                throw new ArgumentNullException();
            }
            catch
            {
                return false;
            }
        }
    }
}

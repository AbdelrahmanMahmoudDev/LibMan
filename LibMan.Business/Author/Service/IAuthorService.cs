namespace LibMan.Business.Author.Service
{
    public interface IAuthorService
    {
        public Task<List<Domains.Author>> GetAllAuthors();
        public Task<List<string>> GetAllAuthorNames();
        public Task<Domains.Author> GetAuthorBasedOnId(int id);
        public Task<bool> SaveNew(Domains.Author newAuthor);
        public Task<bool> SaveUpdate(Domains.Author newAuthor);
        public Task<bool> RemoveAuthor(int id);
    }
}

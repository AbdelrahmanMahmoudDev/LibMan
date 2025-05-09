namespace LibMan.Data.Repository.CustomRepository.Author
{
    public interface IAuthorRepository : IRepository<Domains.Author>
    {
        Task<List<string>> GetAllAuthorNames();
    }
}

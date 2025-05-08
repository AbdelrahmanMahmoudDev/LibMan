namespace LibMan.Business.Author
{
    public interface IAuthorService
    {
        public Task<List<Data.Models.Author>> GetAllAuthors();
    }
}

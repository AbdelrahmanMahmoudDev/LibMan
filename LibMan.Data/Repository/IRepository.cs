namespace LibMan.Data.Repository
{
    internal interface IRepository<T> where T : class
    {
        public Task<T> GetByIdAsync(int id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<bool> AddAsync(T entity);
        public bool Update(T entity);
        public bool Delete(T entity);
    }
}

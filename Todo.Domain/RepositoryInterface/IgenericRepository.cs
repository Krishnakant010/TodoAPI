namespace Todo.Domain.RepositoryInterface;

public interface IgenericRepository<TDomain>where TDomain: class
{
    Task <TDomain> GetByIdAsync(object id);
    
    Task<IEnumerable<TDomain>> GetAllAsync();
    
    Task CreateAsync(TDomain domain);
    Task AddAsync(TDomain domain);
    Task<int> CommitAsync();
    Task UpdateAsync(TDomain domain);
}
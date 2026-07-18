using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.RepositoryInterface;
using Todo.Infrastructure.Persistence.Entities;

namespace Todo.Infrastructure.Repository;

public class GenericRepository<TDomain, TEntity>(TodoAppDbContext todoAppDbContext,IMapper mapper) : IgenericRepository<TDomain>
    where TDomain : class where TEntity:class
{
    private readonly TodoAppDbContext _dbContext = todoAppDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<TDomain?> GetByIdAsync(object id)
    {
        //Tenentiy can be User for eg
        var entity = _dbContext.Set<TEntity>().FindAsync(id);
        return entity ==null? null : _mapper.Map<TDomain>(entity);
    }

    public async Task<IEnumerable<TDomain>> GetAllAsync()
    {
        return await _dbContext.Set<TEntity>().ProjectTo<TDomain>(_mapper.ConfigurationProvider).ToListAsync();
    }
 
    public Task CreateAsync(TDomain domain)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(TDomain domain)
    {
        var entity = _mapper.Map<TEntity>(domain);
         await _dbContext.Set<TEntity>().AddAsync(entity);
    }

    public async Task<int> CommitAsync()
    {
       return  await _dbContext.SaveChangesAsync();
    }

    public Task UpdateAsync(TDomain domain)
    {
        throw new NotImplementedException();
    }
}


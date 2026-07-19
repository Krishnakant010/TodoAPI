using Todo.Domain.DomainEntities;

namespace Todo.Domain.RepositoryInterface;

public interface IUserRepository :IgenericRepository<UserDomain>
{
    Task<UserDomain> GetByEmailAsync(string emailAddress);
}
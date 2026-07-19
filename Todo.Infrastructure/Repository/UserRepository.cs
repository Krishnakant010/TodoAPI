using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.DomainEntities;
using Todo.Domain.RepositoryInterface;
using Todo.Infrastructure.Persistence.Entities;

namespace Todo.Infrastructure.Repository;

public class UserRepository(TodoAppDbContext todoAppDbContext, IMapper mapper) :GenericRepository<UserDomain,User>(todoAppDbContext, mapper),IUserRepository
{
    public async Task<UserDomain> GetByEmailAsync(
        string emailAddress)
    {

        var user = 
            await todoAppDbContext.Users.FirstOrDefaultAsync(
                x => x.Email == emailAddress);

        return mapper.Map<UserDomain>(user);
    }
}
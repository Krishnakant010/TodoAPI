using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Infrastructure.Persistence.Entities;

namespace Todo.Infrastructure.Configurations;

public class UserConfiguration:IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x=>x.Email).IsRequired().HasMaxLength(256);
        builder.Property(x=>x.FullName).IsRequired().HasMaxLength(256);
    }
}
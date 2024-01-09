using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AngryBot.Domain.Entities;

namespace AngryBot.Persistence.Repositories.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever().IsRequired();
        builder.Property(x => x.Username).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
    }
}
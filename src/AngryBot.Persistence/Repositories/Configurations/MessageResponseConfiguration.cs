using AngryBot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngryBot.Persistence.Repositories.Configurations;

internal sealed class MessageResponseConfiguration : IEntityTypeConfiguration<MessageResponse>
{
    public void Configure(EntityTypeBuilder<MessageResponse> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Message).IsRequired();
        builder.Property(x => x.Response).IsRequired();
        builder.Property(x => x.ResponseType).IsRequired();
        builder.HasOne(x => x.Guild).WithMany().IsRequired();
        builder.Property(x => x.AddedAt).IsRequired();
        builder.HasOne(x => x.AddedBy).WithMany().IsRequired();
    }
}

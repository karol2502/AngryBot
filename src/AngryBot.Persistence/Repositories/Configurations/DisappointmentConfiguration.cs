using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AngryBot.Domain.Entities;

namespace AngryBot.Persistence.Repositories.Configurations;

internal sealed class DisappointmentConfiguration : IEntityTypeConfiguration<Disappointment>
{
    public void Configure(EntityTypeBuilder<Disappointment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.AddedAt).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.HasOne(x => x.UserDisappointment).WithMany(x => x.Disappointments).IsRequired();
        builder.HasOne(x => x.AddedBy).WithMany().IsRequired();
    }
}
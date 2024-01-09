using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AngryBot.Domain.Entities;

namespace AngryBot.Persistence.Repositories.Configurations;

internal sealed class UserDisappointmentConfiguration : IEntityTypeConfiguration<UserDisappointment>
{
    public void Configure(EntityTypeBuilder<UserDisappointment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.User).WithOne();
        builder.HasMany(x => x.Disappointments).WithOne(x => x.UserDisappointment);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wisdom.Entities;

namespace Wisdom.Persistence.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Table name
        builder.ToTable("Users");
        
        // Attributes Configurations
        builder.HasKey(user => user.Id);
        builder.Property(user => user.Id).UseIdentityColumn();
        builder.Property(user => user.Name).HasMaxLength(50).IsRequired();
        builder.HasIndex(user => user.Email);
        builder.Property(user => user.Email).HasMaxLength(150).IsRequired();
        builder.Property(user => user.Password).HasMaxLength(100).IsRequired();
    }
}
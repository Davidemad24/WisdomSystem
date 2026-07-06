using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wisdom.Entities;

namespace Wisdom.Persistence.Configurations;

public class RefreshTokenConfigurations : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        // Table name
        builder.ToTable("RefreshTokens");
        
        // Attributes Configurations
        builder.HasKey(refreshToken => refreshToken.Id);
        builder.Property(refreshToken => refreshToken.Id).UseIdentityColumn();
        builder.HasIndex(refreshToken => refreshToken.Token);
        builder.Property(refreshToken => refreshToken.Token).HasMaxLength(150).IsRequired();

        // Relationships Configurations
        builder.HasOne(refreshToken => refreshToken.User).WithMany(user => user.RefreshTokens)
            .HasForeignKey(refreshToken => refreshToken.UserId).OnDelete(DeleteBehavior.Cascade);
    }
}
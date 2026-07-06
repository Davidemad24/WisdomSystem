using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wisdom.Persistence.Configurations;

public class WisdomConfigurations : IEntityTypeConfiguration<Entities.Wisdom>
{
    public void Configure(EntityTypeBuilder<Entities.Wisdom> builder)
    {
        // Table name
        builder.ToTable("Wisdoms");
        
        // Attributes Configurations
        builder.HasKey(wisdom => wisdom.Id);
        builder.Property(wisdom => wisdom.Id).UseIdentityColumn();
        builder.Property(wisdom => wisdom.Content).HasMaxLength(200).IsRequired();
        builder.Property(wisdom => wisdom.UserId).IsRequired();
        
        // Relationships Configurations
        builder.HasOne(wisdom => wisdom.User).WithMany(user => user.Wisdoms)
            .HasForeignKey(wisdom => wisdom.UserId).OnDelete(DeleteBehavior.Cascade);
    }
}
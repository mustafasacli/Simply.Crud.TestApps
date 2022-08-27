using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookmarks.Project.EntityConfiguration
{
    public class BrowsersConfigurations : EntityTypeConfiguration<Browsers>
    {
        public BrowsersConfigurations()
        {
            this.HasKey(p => p.Id)

            this.Property(e => e.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .HasColumnOrder(1)
            .IsRequired()
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(e => e.Name)
            .HasColumnName("Name")
            .HasColumnType("nvarchar")
            .HasColumnOrder(2)
            .IsRequired()
            .HasMaxLength(100);

            this.Property(e => e.Path)
            .HasColumnName("Path")
            .HasColumnType("nvarchar")
            .HasColumnOrder(3)
            .IsRequired()
            .HasMaxLength(500);
        }
    }
}
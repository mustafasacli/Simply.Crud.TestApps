using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookmarks.Project.EntityConfiguration
{
    public class BrowsersConfigurations : IEntityTypeConfiguration<Browsers>
    {
        public void Configure(EntityTypeBuilder<Browsers> builder)
        {
            builder.HasKey(e => e.Id)

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("int")
                .HasColumnOrder(1)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar")
                .HasColumnOrder(2)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Path)
                .HasColumnName("Path")
                .HasColumnType("nvarchar")
                .HasColumnOrder(3)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
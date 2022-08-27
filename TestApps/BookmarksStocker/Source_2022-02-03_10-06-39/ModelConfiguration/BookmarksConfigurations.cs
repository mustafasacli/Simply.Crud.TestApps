using System;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookmarks.Project.EntityConfiguration
{
    public class BookmarksConfigurations : EntityTypeConfiguration<Bookmarks>
    {
        public BookmarksConfigurations()
        {
            this.HasKey(p => p.Id)

            this.Property(e => e.Id)
            .HasColumnName("ID")
            .HasColumnType("bigint")
            .HasColumnOrder(1)
            .IsRequired()
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(e => e.Name)
            .HasColumnName("Name")
            .HasColumnType("nvarchar")
            .HasColumnOrder(2)
            .IsRequired()
            .HasMaxLength(100);

            this.Property(e => e.Description)
            .HasColumnName("Description")
            .HasColumnType("nvarchar")
            .HasColumnOrder(3)
            .IsOptional()
            .HasMaxLength(500);

            this.Property(e => e.Url)
            .HasColumnName("Url")
            .HasColumnType("nvarchar")
            .HasColumnOrder(4)
            .IsRequired()
            .HasMaxLength(500);

            this.Property(e => e.CreationTime)
            .HasColumnName("CreationTime")
            .HasColumnType("datetime")
            .HasColumnOrder(5)
            .IsRequired();

            this.Property(e => e.UpdateTime)
            .HasColumnName("UpdateTime")
            .HasColumnType("datetime")
            .HasColumnOrder(6)
            .IsOptional();
        }
    }
}
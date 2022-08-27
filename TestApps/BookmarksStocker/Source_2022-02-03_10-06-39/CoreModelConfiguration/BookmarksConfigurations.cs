using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookmarks.Project.EntityConfiguration
{
    public class BookmarksConfigurations : IEntityTypeConfiguration<Bookmarks>
    {
        public void Configure(EntityTypeBuilder<Bookmarks> builder)
        {
            builder.HasKey(e => e.Id)

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasColumnType("bigint")
                .HasColumnOrder(1)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar")
                .HasColumnOrder(2)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Description)
                .HasColumnName("Description")
                .HasColumnType("nvarchar")
                .HasColumnOrder(3)
                .IsOptional()
                .HasMaxLength(500);

            builder.Property(e => e.Url)
                .HasColumnName("Url")
                .HasColumnType("nvarchar")
                .HasColumnOrder(4)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(e => e.CreationTime)
                .HasColumnName("CreationTime")
                .HasColumnType("datetime")
                .HasColumnOrder(5)
                .IsRequired();

            builder.Property(e => e.UpdateTime)
                .HasColumnName("UpdateTime")
                .HasColumnType("datetime")
                .HasColumnOrder(6)
                .IsOptional();
        }
    }
}
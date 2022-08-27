using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookmarks.Project.Entity
{
    [Table("Bookmarks")]
    public class Bookmarks
    {
        /// <summary>
        /// Gets or Sets the Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Id alanýna veri girilmelidir.")]
        [Column("ID", Order = 1, TypeName = "bigint")]
        public long Id
        { get; set; }

        /// <summary>
        /// Gets or Sets the Name
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name alanýna veri girilmelidir.")]
        [StringLength(100, ErrorMessage = "Name alaný 100 karakterden uzun olamaz.")]
        [Column("Name", Order = 2, TypeName = "nvarchar")]
        public string Name
        { get; set; }

        /// <summary>
        /// Gets or Sets the Description
        /// </summary>
        [StringLength(500, ErrorMessage = "Description alaný 500 karakterden uzun olamaz.")]
        [Column("Description", Order = 3, TypeName = "nvarchar")]
        public string Description
        { get; set; }

        /// <summary>
        /// Gets or Sets the Url
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Url alanýna veri girilmelidir.")]
        [StringLength(500, ErrorMessage = "Url alaný 500 karakterden uzun olamaz.")]
        [Column("Url", Order = 4, TypeName = "nvarchar")]
        public string Url
        { get; set; }

        /// <summary>
        /// Gets or Sets the CreationTime
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "CreationTime alanýna veri girilmelidir.")]
        [Column("CreationTime", Order = 5, TypeName = "datetime")]
        public DateTime CreationTime
        { get; set; }

        /// <summary>
        /// Gets or Sets the UpdateTime
        /// </summary>
        [Column("UpdateTime", Order = 6, TypeName = "datetime")]
        public DateTime? UpdateTime
        { get; set; }
    }
}
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
        [Required(AllowEmptyStrings = false, ErrorMessage = "Id alan�na veri girilmelidir.")]
        [Column("ID", Order = 1, TypeName = "bigint")]
        public long Id
        { get; set; }

        /// <summary>
        /// Gets or Sets the Name
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name alan�na veri girilmelidir.")]
        [StringLength(100, ErrorMessage = "Name alan� 100 karakterden uzun olamaz.")]
        [Column("Name", Order = 2, TypeName = "nvarchar")]
        public string Name
        { get; set; }

        /// <summary>
        /// Gets or Sets the Description
        /// </summary>
        [StringLength(500, ErrorMessage = "Description alan� 500 karakterden uzun olamaz.")]
        [Column("Description", Order = 3, TypeName = "nvarchar")]
        public string Description
        { get; set; }

        /// <summary>
        /// Gets or Sets the Url
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Url alan�na veri girilmelidir.")]
        [StringLength(500, ErrorMessage = "Url alan� 500 karakterden uzun olamaz.")]
        [Column("Url", Order = 4, TypeName = "nvarchar")]
        public string Url
        { get; set; }

        /// <summary>
        /// Gets or Sets the CreationTime
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "CreationTime alan�na veri girilmelidir.")]
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
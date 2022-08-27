using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookmarksStocker.Source.Entity
{
    [Table("Browsers")]
    public class Browsers
    {
        /// <summary>
        /// Gets or Sets the Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Id alan�na veri girilmelidir.")]
        [Column("Id", Order = 1, TypeName = "int")]
        public int Id
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
        /// Gets or Sets the Path
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Path alan�na veri girilmelidir.")]
        [StringLength(500, ErrorMessage = "Path alan� 500 karakterden uzun olamaz.")]
        [Column("Path", Order = 3, TypeName = "nvarchar")]
        public string Path
        { get; set; }
    }
}
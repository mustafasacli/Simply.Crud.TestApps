using System.ComponentModel.DataAnnotations;

namespace Bookmarks.Project.ViewModel
{
    public class BrowsersViewModel
    {
        /// <summary>
        /// Gets or Sets the Id
        /// </summary>
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Id alan�na veri girilmelidir.")]
        public int Id
        { get; set; }

        /// <summary>
        /// Gets or Sets the Name
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name alan�na veri girilmelidir.")]
        [StringLength(100, ErrorMessage = "Name alan� 100 karakterden uzun olamaz.")]
        public string Name
        { get; set; }

        /// <summary>
        /// Gets or Sets the Path
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Path alan�na veri girilmelidir.")]
        [StringLength(500, ErrorMessage = "Path alan� 500 karakterden uzun olamaz.")]
        public string Path
        { get; set; }
    }
}
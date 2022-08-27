using System;
using System.ComponentModel.DataAnnotations;

namespace BookmarksStocker.Source.ViewModel
{
    public class BookmarksViewModel
    {
        /// <summary>
        /// Gets or Sets the Id
        /// </summary>
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Id alan�na veri girilmelidir.")]
        public long Id
        { get; set; }

        /// <summary>
        /// Gets or Sets the Name
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name alan�na veri girilmelidir.")]
        [StringLength(100, ErrorMessage = "Name alan� 100 karakterden uzun olamaz.")]
        public string Name
        { get; set; }

        /// <summary>
        /// Gets or Sets the Description
        /// </summary>
        [StringLength(500, ErrorMessage = "Description alan� 500 karakterden uzun olamaz.")]
        public string Description
        { get; set; }

        /// <summary>
        /// Gets or Sets the Url
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Url alan�na veri girilmelidir.")]
        [StringLength(500, ErrorMessage = "Url alan� 500 karakterden uzun olamaz.")]
        public string Url
        { get; set; }

        /// <summary>
        /// Gets or Sets the CreationTime
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "CreationTime alan�na veri girilmelidir.")]
        public DateTime CreationTime
        { get; set; }

        /// <summary>
        /// Gets or Sets the UpdateTime
        /// </summary>
        public DateTime? UpdateTime
        { get; set; }
    }
}
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
        [Required(AllowEmptyStrings = false, ErrorMessage = "Id alanýna veri girilmelidir.")]
        public long Id
        { get; set; }

        /// <summary>
        /// Gets or Sets the Name
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name alanýna veri girilmelidir.")]
        [StringLength(100, ErrorMessage = "Name alaný 100 karakterden uzun olamaz.")]
        public string Name
        { get; set; }

        /// <summary>
        /// Gets or Sets the Description
        /// </summary>
        [StringLength(500, ErrorMessage = "Description alaný 500 karakterden uzun olamaz.")]
        public string Description
        { get; set; }

        /// <summary>
        /// Gets or Sets the Url
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Url alanýna veri girilmelidir.")]
        [StringLength(500, ErrorMessage = "Url alaný 500 karakterden uzun olamaz.")]
        public string Url
        { get; set; }

        /// <summary>
        /// Gets or Sets the CreationTime
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "CreationTime alanýna veri girilmelidir.")]
        public DateTime CreationTime
        { get; set; }

        /// <summary>
        /// Gets or Sets the UpdateTime
        /// </summary>
        public DateTime? UpdateTime
        { get; set; }
    }
}
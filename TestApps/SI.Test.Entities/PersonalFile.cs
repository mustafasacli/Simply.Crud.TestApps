namespace SI.Test.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="PersonalFile"/>.
    /// </summary>
    [Table("PersonalFile")]
    public partial class PersonalFile
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // [Column("ID")]
        public long Id
        { get; set; }

        /// <summary>
        /// Gets or sets the FileName.
        /// </summary>
        [Required]
        [StringLength(100)]
        //[Column("FILENAME")]
        public string FileName
        { get; set; }

        /// <summary>
        /// Gets or sets the FileContent.
        /// </summary>
        [Required]
        //[Column("FILECONTENT")]
        public byte[] FileContent
        { get; set; }

        //[Column("CREATEDBY")]
        /// <summary>
        /// Gets or sets the CreatedBy.
        /// </summary>
        public long CreatedBy
        { get; set; }

        //[Column("CREATIONDATE")]
        //public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the CreationDate.
        /// </summary>
        public string CreationDate
        { get; set; }

        //[Column("ISACTIVE")]
        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        public long IsActive
        { get; set; }

        /// <summary>
        /// Gets or sets the User.
        /// </summary>
        public virtual User User
        { get; set; }
    }
}
namespace SI.Test.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="User"/>.
    /// </summary>
    [Table("user", Schema = "public")]
    public partial class User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
            LogEntries = new HashSet<LogEntry>();
            PersonalFiles = new HashSet<PersonalFile>();
            TransactionLogs = new HashSet<TransactionLog>();
        }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id
        { get; set; }

        /// <summary>
        /// Gets or sets the FirstName.
        /// </summary>
        [Required]
        [StringLength(50)]
        [Column("first_name")]
        public string FirstName
        { get; set; }

        /// <summary>
        /// Gets or sets the LastName.
        /// </summary>
        [Required]
        [StringLength(50)]
        [Column("last_name")]
        public string LastName
        { get; set; }

        /// <summary>
        /// Gets or sets the UserName.
        /// </summary>
        [StringLength(50)]
        [Column("user_name")]
        public string UserName
        { get; set; }

        /// <summary>
        /// Gets or sets the Pass.
        /// </summary>
        [Required]
        [StringLength(50)]
        [Column("pass")]
        public string Pass
        { get; set; }

        /// <summary>
        /// Gets or sets the UserType.
        /// </summary>
        [Column("user_type")]
        public int UserType
        { get; set; }

        /// <summary>
        /// Gets or sets the CreationDate.
        /// </summary>
        [Column("created_on")]
        public DateTime CreationDate
        { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsActive.
        /// </summary>
        [Column("is_active")]
        public byte IsActive
        { get; set; }

        /// <summary>
        /// Gets or sets the ıs deleted.
        /// </summary>
        [Column("is_deleted")]
        public byte IsDeleted
        { get; set; }

        /// <summary>
        /// Gets or sets the update time.
        /// </summary>
        [Column("updated_on")]
        public DateTime UpdateTime
        { get; set; }

        /// <summary>
        /// Gets or sets the user type name.
        /// </summary>
        [NotMapped]
        public string UserTypeName
        { get; set; }

        /// <summary>
        /// Gets or sets the LogEntries.
        /// </summary>
        public virtual ICollection<LogEntry> LogEntries
        { get; set; }

        /// <summary>
        /// Gets or sets the PersonalFiles.
        /// </summary>
        public virtual ICollection<PersonalFile> PersonalFiles
        { get; set; }

        /// <summary>
        /// Gets or sets the TransactionLogs.
        /// </summary>
        public virtual ICollection<TransactionLog> TransactionLogs
        { get; set; }

        /// <summary>
        /// Gets or sets the UserType1.
        /// </summary>
        public virtual UserType UserType1
        { get; set; }
    }
}
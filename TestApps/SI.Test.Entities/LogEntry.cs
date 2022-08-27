namespace SI.Test.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="LogEntry"/>.
    /// </summary>
    [Table("LogEntry", Schema = "log")]
    public partial class LogEntry
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        { get; set; }

        /// <summary>
        /// Gets or sets the UserId.
        /// </summary>
        public int? UserId
        { get; set; }

        /// <summary>
        /// Gets or sets the LogTime.
        /// </summary>
        public DateTime LogTime
        { get; set; }

        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        [StringLength(100)]
        public string Title
        { get; set; }

        /// <summary>
        /// Gets or sets the ErrorCode.
        /// </summary>
        [StringLength(50)]
        public string ErrorCode
        { get; set; }

        /// <summary>
        /// Gets or sets the Message.
        /// </summary>
        [StringLength(100)]
        public string Message
        { get; set; }

        /// <summary>
        /// Gets or sets the StackTrace.
        /// </summary>
        public string StackTrace
        { get; set; }

        /// <summary>
        /// Gets or sets the User.
        /// </summary>
        public virtual User User
        { get; set; }
    }
}
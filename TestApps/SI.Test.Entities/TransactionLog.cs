namespace SI.Test.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="TransactionLog"/>.
    /// </summary>
    [Table("TransactionLog", Schema = "log")]
    public partial class TransactionLog
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
        /// Gets or sets the TableName.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string TableName
        { get; set; }

        /// <summary>
        /// Gets or sets the EntityId.
        /// </summary>
        public int? EntityId
        { get; set; }

        /// <summary>
        /// Gets or sets the TransactionType.
        /// </summary>
        [StringLength(50)]
        public string TransactionType
        { get; set; }

        /// <summary>
        /// Gets or sets the User.
        /// </summary>
        public virtual User User
        { get; set; }
    }
}
namespace SI.Test.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="UserType"/>.
    /// </summary>
    [Table("user_type", Schema = "public")]
    public partial class UserType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserType"/> class.
        /// </summary>
        public UserType()
        {
            Users = new HashSet<User>();
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
        /// Gets or sets the TypeName.
        /// </summary>
        [Required]
        [StringLength(50)]
        [Column("type_name")]
        public string TypeName
        { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsActive.
        /// </summary>
        [Column("is_active")]
        public byte IsActive
        { get; set; }

        /// <summary>
        /// Gets or sets the Users.
        /// </summary>
        public virtual ICollection<User> Users
        { get; set; }
    }
}
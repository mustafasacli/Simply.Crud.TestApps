namespace SI.Test.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="UserType"/>.
    /// </summary>
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
        public int Id
        { get; set; }

        /// <summary>
        /// Gets or sets the TypeName.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string TypeName
        { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsActive.
        /// </summary>
        public bool IsActive
        { get; set; }

        /// <summary>
        /// Gets or sets the Users.
        /// </summary>
        public virtual ICollection<User> Users
        { get; set; }
    }
}
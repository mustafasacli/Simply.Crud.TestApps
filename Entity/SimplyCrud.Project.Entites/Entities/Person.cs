using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimplyCrud.Project.Entity
{
    [Table("person", Schema = "crud_test_db")]
    public class Person
    {
        /// <summary>
        /// Gets or Sets the Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Id alanýna veri girilmelidir.")]
        [Column("id", Order = 1, TypeName = "bigint")]
        public long Id
        { get; set; }

        /// <summary>
        /// Gets or Sets the FirstName
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "FirstName alanýna veri girilmelidir.")]
        [StringLength(100, ErrorMessage = "FirstName alaný 100 karakterden uzun olamaz.")]
        [Column("first_name", Order = 2, TypeName = "varchar")]
        public string FirstName
        { get; set; }

        /// <summary>
        /// Gets or Sets the LastName
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "LastName alanýna veri girilmelidir.")]
        [StringLength(100, ErrorMessage = "LastName alaný 100 karakterden uzun olamaz.")]
        [Column("last_name", Order = 3, TypeName = "varchar")]
        public string LastName
        { get; set; }

        /// <summary>
        /// Gets or Sets the FullName
        /// </summary>
        [StringLength(200, ErrorMessage = "FullName alaný 200 karakterden uzun olamaz.")]
        [Column("full_name", Order = 4, TypeName = "varchar")]
        [NotMapped]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string FullName
        { get; set; }

        /// <summary>
        /// Gets or Sets the Gender
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Gender alanýna veri girilmelidir.")]
        [Column("gender", Order = 5, TypeName = "char")]
        public char Gender
        { get; set; }

        /// <summary>
        /// Gets or Sets the SsNo
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "SsNo alanýna veri girilmelidir.")]
        [StringLength(20, ErrorMessage = "SsNo alaný 20 karakterden uzun olamaz.")]
        [Column("ssno", Order = 6, TypeName = "varchar")]
        public string SsNo
        { get; set; }

        /// <summary>
        /// Gets or Sets the BirthDate
        /// </summary>
        [Column("birth_date", Order = 7, TypeName = "date")]
        public DateTime? BirthDate
        { get; set; }

        /// <summary>
        /// Gets or Sets the BirthDateInt
        /// </summary>
        [Column("birth_date_int", Order = 8, TypeName = "int")]
        [NotMapped]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? BirthDateInt
        { get; set; }

        /// <summary>
        /// Gets or Sets the IsDeleted
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "IsDeleted alanýna veri girilmelidir.")]
        [Column("is_deleted", Order = 9, TypeName = "tinyint")]
        public bool IsDeleted
        { get; set; }

        /// <summary>
        /// Gets or Sets the IsActive
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "IsActive alanýna veri girilmelidir.")]
        [Column("is_active", Order = 10, TypeName = "tinyint")]
        public bool IsActive
        { get; set; } = true;

        private DateTime? _createdOn = null;
        /// <summary>
        /// Gets or Sets the CreatedOn
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "CreatedOn alanýna veri girilmelidir.")]
        [Column("created_on", Order = 11, TypeName = "datetime")]
        public DateTime CreatedOn
        {
            get
            {
                _createdOn = _createdOn ?? DateTime.Now;
                return _createdOn.Value;
            }
            set { _createdOn = value; }
        }

        /// <summary>
        /// Gets or Sets the CreatedOnTick
        /// </summary>
        [Column("created_on_tick", Order = 12, TypeName = "bigint")]
        [NotMapped]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public long? CreatedOnTick
        { get; set; }

        /// <summary>
        /// Gets or Sets the CreatedBy
        /// </summary>
        [Column("created_by", Order = 13, TypeName = "bigint")]
        public long? CreatedBy
        { get; set; }

        /// <summary>
        /// Gets or Sets the UpdatedOn
        /// </summary>
        [Column("updated_on", Order = 14, TypeName = "datetime")]
        public DateTime? UpdatedOn
        { get; set; }

        /// <summary>
        /// Gets or Sets the UpdatedOnTick
        /// </summary>
        [Column("updated_on_tick", Order = 15, TypeName = "bigint")]
        [NotMapped]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public long? UpdatedOnTick
        { get; set; }

        /// <summary>
        /// Gets or Sets the UpdatedBy
        /// </summary>
        [Column("updated_by", Order = 16, TypeName = "bigint")]
        public long? UpdatedBy
        { get; set; }
    }
}
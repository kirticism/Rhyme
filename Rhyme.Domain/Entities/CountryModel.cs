using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rhyme.Domain.Entities
{
    [Table("mst_country")]

    public class CountryModel
    {
        [Key]
        [Column("country_id")]
        [Required]
        public int countryId { get; set; }


        [Column("country_name", TypeName = "varchar(50)")]
        [Required(ErrorMessage = "Please Enter Country Name")]
        public string? countryName { get; set; }


        [Column("country_code", TypeName = "varchar(20)")]
        [Required(ErrorMessage = "Please Enter Country Code")]
        public string? countryCode { get; set; }


        [Column("is_anchor_tagged")]

        public Boolean isAnchorTagged { get; set; } = false;


        [DefaultValue("false")]
        [Column("is_restricted", TypeName = "BIT")]

        public Boolean isRestricted { get; set; } = false;


        [DefaultValue("false")]
        [Column("is_excluded", TypeName = "BIT")]

        public Boolean isExcluded { get; set; } = false;


        [Column("from_date", TypeName = "datetime")]
        [Required(ErrorMessage = "Please Enter from date")]
        public DateTime? fromDate { get; set; }


        [Column("to_date", TypeName = "datetime")]
        public DateTime? toDate { get; set; }


        [Column("target_in_cr")]
        [RegularExpression(RegularExpressions.target, ErrorMessage = "Enter valid Country Code")]
        public int? targetInCr { get; set; }


        [Column("is_used")]
        [DefaultValue(null)]
        public Boolean? isUsed { get; set; }

        [Column("is_active")]
        [DefaultValue(true)]

        public bool? isActive { get; set; }
        [Column("is_deleted")]
        [DefaultValue(false)]

        public bool? isDeleted { get; set; }
        [Column("created_by")]
        public Guid createdBy { get; set; }


        [Column("created_date")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? createdDate { get; set; }


        [Column("updated_by")]
        public Guid? updatedBy { get; set; }


        [Column("updated_date")]
        public DateTime? updatedDate { get; set; }


        [Column("authorized_by")]
        public Guid? authorizedBy { get; set; }


        [Column("authorized_date")]
        public DateTime? authorizedDate { get; set; }


        [Column("status")]
        public int? status { get; set; }


        [Column("sequence")]
        public int? sequence { get; set; }


        [Column("version")]
        public int? version { get; set; }


        [Column("description")]
        public string? description { get; set; }


        [Column("company_id")]
        public int? companyId { get; set; }

        
        [Column("environment")]
        public int? environment { get; set; }
    }
}
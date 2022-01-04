using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OsigDrustvo.Models
{
    public class Partner
    {

        public int PartnerId { get; set; }

        [Required(ErrorMessage = "Required", AllowEmptyStrings = false)]
        [Display(Name = "First name")]
        [RegularExpression(@"^[a-zčćđšžA-ZČĆĐŠŽ0-9]*$", ErrorMessage = "Only alfanumeric characters")]
        [StringLength(255, ErrorMessage = "Must be between 2 and 255 character lenght. Only alfanumeric characters", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Last name")]
        [RegularExpression(@"^[a-zčćđšžA-ZČĆĐŠŽ0-9]*$", ErrorMessage = "Only alfanumeric characters")]
        [StringLength(255, ErrorMessage = "Must be between 2 and 255 character lenght", MinimumLength = 2)]
        public string LastName { get; set; }

        [NotMapped]
        [Display(Name = "FullName")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }


        [RegularExpression(@"^[a-zčćđšžA-ZČĆĐŠŽ0-9 ,]*$")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Partner number")]
        [RegularExpression(@"^\d{20}$", ErrorMessage = "Must be exactly 20 numbers")]
        public string PartnerNumber { get; set; }

        [RegularExpression(@"^$|^[0-9]{11}$", ErrorMessage = "Croatian OIB must be 11 numbers")]
        [Display(Name = "CroatianPIN")]
        public string CroatianPin { get; set; }



        [Required(ErrorMessage = "Required")]
        [Range(1, 2, ErrorMessage = "Required")]
        public int PartnerTypeId { get; set; }



        [DataType(DataType.Date)]
        public DateTime CreatedAtUtc { get; set; }

        [Display(Name = "Created by user (email)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [StringLength(255, ErrorMessage = "Maximum lenght is 255 character")]
        public string CreateByUser { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public bool IsForeign { get; set; }


        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^[a-zA-Z0-9]*$")]
        [Remote("IsExternalCodeAviable", "Default", ErrorMessage = "External code already exist")]
        public string ExternalCode { get; set; }

        [Required(ErrorMessage = "Required")]
        [UIHint("TemplateGender")]
        public string Gender { get; set; }

    }
}
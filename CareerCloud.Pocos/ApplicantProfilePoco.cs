using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Profiles")]
   public class ApplicantProfilePoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Login { get; set; }
        [Column("Current_Salary")]
        public decimal? CurrentSalary { get; set; }
        [Column("Current_Rate")]
        public decimal? CurrentRate { get; set; }
        public string Currency { get; set; }
        [Column("Country_Code")]
        public string Country { get; set; }
        [Column("State_Province_Code")]
        public string Province { get; set; }

        [Column("Street_Address")]
        public string Street { get; set; }
        [Column("City_Town")]
        public string City { get; set; }
        [Column("Zip_Postal_Code")]
        public string PostalCode { get; set; }
        [Timestamp]
        [Column("Time_Stamp")]
        public Byte[] TimeStamp { get; set; }
        public virtual ICollection<ApplicantEducationPoco> AppEducation { get; set; }
        public virtual ICollection<ApplicantJobApplicationPoco> AppJobApplication { get; set; }
        public virtual SecurityLoginPoco SecurityLogin { get; set; }
        public virtual SystemCountryCodePoco SysCountryCode { get; set; }
        public virtual ICollection<ApplicantResumePoco> AppResumes { get; set; }
        public virtual ICollection<ApplicantSkillPoco> AppSkills { get; set; }

        public virtual ICollection<ApplicantWorkHistoryPoco> AppWorkHistory { get; set; }
    }
}

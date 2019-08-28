using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("System_Country_Codes")]
    public class SystemCountryCodePoco 
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ApplicantProfilePoco> AppProfile { get; set; }
        public virtual ICollection<ApplicantWorkHistoryPoco> AppWorkHistory { get; set; }
    }
}

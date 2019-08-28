using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.SqlServer;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class CareerCloudContext : DbContext
    {

        public CareerCloudContext() : base(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString)
        {
            var ensureDllIsCopied = SqlProviderServices.Instance;
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<SystemCountryCodePoco>()
    .HasMany(c => c.AppProfile)
    .WithOptional(b => b.SysCountryCode)
    .HasForeignKey(c => c.Country);


            modelBuilder.Entity<SystemCountryCodePoco>()
                .HasMany(c => c.AppWorkHistory)
                .WithRequired(b => b.SysCountryCode)
                .HasForeignKey(c => c.CountryCode)
                .WillCascadeOnDelete(true);


            modelBuilder.Entity<SystemLanguageCodePoco>()
                .HasMany(c => c.CompDescription)
                .WithRequired(b => b.SysLanguageCode)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<CompanyProfilePoco>()
                .HasMany(c => c.CompDescription)
                .WithRequired(b => b.CompProfile)
                .HasForeignKey(c => c.Company)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<CompanyProfilePoco>()
                .HasMany(c => c.CompJob)
                .WithRequired(b => b.CompProfile)
                .HasForeignKey(c => c.Company)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<CompanyProfilePoco>()
                .HasMany(c => c.CompLocation)
                .WithRequired(b => b.CompProfile)
                .HasForeignKey(c => c.Company)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<CompanyJobPoco>()
                .HasMany(c => c.AppJobApplication)
                .WithRequired(b => b.CompanyJob)
                .HasForeignKey(c => c.Job)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<CompanyJobPoco>()
                .HasMany(c => c.CompJobEducation)
                .WithRequired(b => b.CompJob)
                .HasForeignKey(c => c.Job)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<CompanyJobPoco>()
                .HasMany(c => c.CompJobSkill)
                .WithRequired(b => b.CompJob)
                .HasForeignKey(c => c.Job)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<CompanyJobPoco>()
                .HasMany(c => c.CompJobDescription)
                .WithRequired(b => b.CompJob)
                .HasForeignKey(c => c.Job)
                .WillCascadeOnDelete(true);


            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(c => c.AppEducation)
                .WithRequired(b => b.ApplicantProfile)
                .HasForeignKey(c => c.Applicant)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(c => c.AppResumes)
                .WithRequired(b => b.AppProfile)
                .HasForeignKey(c => c.Applicant)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(c => c.AppWorkHistory)
                .WithRequired(b => b.AppProfile)
                .HasForeignKey(c => c.Applicant)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(c => c.AppSkills)
                .WithRequired(b => b.AppProfile)
                .HasForeignKey(c => c.Applicant)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<SecurityLoginPoco>()
                .HasMany(c => c.AppProfile)
                .WithRequired(b => b.SecurityLogin)
                .HasForeignKey(c => c.Login)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<SecurityLoginPoco>()
                .HasMany(c => c.SecurityLoginsLog)
                .WithRequired(b => b.SecurityLogin)
                .HasForeignKey(c => c.Login)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<SecurityLoginPoco>()
                .HasMany(c => c.SecurityLoginsRole)
                .WithRequired(b => b.SecurityLogin)
                .HasForeignKey(c => c.Login)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<SecurityRolePoco>()
                .HasMany(c => c.SecurityLoginsRole)
                .WithRequired(b => b.SecurityRole)
                .HasForeignKey(c => c.Role)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ApplicantProfilePoco>()
              .HasMany(c => c.AppJobApplication)
              .WithRequired(b => b.ApplicantProfile)
              .HasForeignKey(c => c.Applicant)
              .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }


        DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }

        DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
        DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
        DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
        DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
        DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistory { get; set; }
        DbSet<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }
        DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
        DbSet<CompanyJobPoco> CompanyJobs { get; set; }
        DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
        DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
        DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }
        DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
        DbSet<SecurityLoginsLogPoco> SecurityLoginsLogs { get; set; }
        DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
        DbSet<SecurityRolePoco> SecurityRoles { get; set; }
        DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }
        DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }


    }
}

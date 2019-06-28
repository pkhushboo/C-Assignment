using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantEducationRepository : IDataRepository<ApplicantEducationPoco>
    {
        public void Add(params ApplicantEducationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach(ApplicantEducationPoco AEPoco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Educations]
                                      ([Id],[Applicant],[Major],[Certificate_Diploma],[Start_Date],[Completion_Date],[Completion_Percent])
                                VALUES
                                      (@Id,@Applicant,@Major,@Certificate_Diploma,@Start_Date,@Completion_Date,@Completion_Percent)";
                    cmd.Parameters.AddWithValue("@Id", AEPoco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", AEPoco.Applicant);
                    cmd.Parameters.AddWithValue("@Major", AEPoco.Major);
                    cmd.Parameters.AddWithValue("@Certificate_Diploma", AEPoco.CertificateDiploma);
                    cmd.Parameters.AddWithValue("@Start_Date", AEPoco.StartDate);
                    cmd.Parameters.AddWithValue("@Completion_Date", AEPoco.CompletionDate);
                    cmd.Parameters.AddWithValue("@Completion_Percent", AEPoco.CompletionPercent);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Id],[Applicant],[Major],[Certificate_Diploma],[Start_Date],[Completion_Date],[Completion_Percent],[Time_Stamp] FROM [dbo].[Applicant_Educations]";
                conn.Open();
                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                ApplicantEducationPoco[] appPocos = new ApplicantEducationPoco[1000];
                while(rdr.Read())
                {
                    ApplicantEducationPoco poco = new ApplicantEducationPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Applicant = rdr.GetGuid(1);
                    poco.Major = rdr.GetString(2);
                    poco.CertificateDiploma = rdr.GetString(3);
                    poco.StartDate = (DateTime)(rdr.IsDBNull(4) ? null : rdr[4]);
                    poco.CompletionDate = (DateTime)(rdr.IsDBNull(5) ? null : rdr[5]);
                    poco.CompletionPercent = (byte?)rdr[6];
                    poco.TimeStamp = (byte[])rdr[7];
                    appPocos[x] = poco;
                    x++;
                }
                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
            foreach(ApplicantEducationPoco item in items)
            {
                    cmd.CommandText = $"DELETE FROM [dbo].[Applicant_Educations] WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
            }
            }
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                foreach (ApplicantEducationPoco AEPoco in items)
                {
                    SqlCommand cmd = new SqlCommand(@"UPDATE [dbo].[Applicant_Educations]
                                                SET 
                                                  Applicant = @Applicant, Major = @Major,Certificate_Diploma = @Certificate_Diploma,Start_Date = @Start_Date,Completion_Date = @Completion_Date,Completion_Percent = @Completion_Percent
                                                WHERE Id =@Id", conn);
                    cmd.Parameters.AddWithValue("@Id", AEPoco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", AEPoco.Applicant);
                    cmd.Parameters.AddWithValue("@Major", AEPoco.Major);
                    cmd.Parameters.AddWithValue("@Certificate_Diploma", AEPoco.CertificateDiploma);
                    cmd.Parameters.AddWithValue("@Start_Date", AEPoco.StartDate);
                    cmd.Parameters.AddWithValue("@Completion_Date", AEPoco.CompletionDate);
                    cmd.Parameters.AddWithValue("@Completion_Percent", AEPoco.CompletionPercent);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}

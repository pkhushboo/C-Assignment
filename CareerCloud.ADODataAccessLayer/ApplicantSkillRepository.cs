using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantSkillRepository : IDataRepository<ApplicantSkillPoco>
    {
        public void Add(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantSkillPoco ASPoco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Skills]
                                     ([Id],[Applicant],[Skill],[Skill_Level],[Start_Month],[Start_Year],[End_Month],[End_Year])
                                 VALUES
                                     (@Id,@Applicant,@Skill,@Skill_Level,@Start_Month,@Start_Year,@End_Month,@End_Year)";
                    cmd.Parameters.AddWithValue("@Id", ASPoco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", ASPoco.Applicant);
                    cmd.Parameters.AddWithValue("@Skill", ASPoco.Skill);
                    cmd.Parameters.AddWithValue("@Skill_Level", ASPoco.SkillLevel);
                    cmd.Parameters.AddWithValue("@Start_Month", ASPoco.StartMonth);
                    cmd.Parameters.AddWithValue("@Start_Year", ASPoco.StartYear);
                    cmd.Parameters.AddWithValue("@End_Month", ASPoco.EndMonth);
                    cmd.Parameters.AddWithValue("@End_Year", ASPoco.EndYear);
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

        public IList<ApplicantSkillPoco> GetAll(params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Id],[Applicant],[Skill],[Skill_Level],[Start_Month],[Start_Year],[End_Month],[End_Year],[Time_Stamp] FROM [dbo].[Applicant_Skills]";
                 conn.Open();
                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                ApplicantSkillPoco[] appPocos = new ApplicantSkillPoco[1000];
                while (rdr.Read())
                {
                    ApplicantSkillPoco poco = new ApplicantSkillPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Applicant = rdr.GetGuid(1);
                    poco.Skill = rdr.GetString(2);
                    poco.SkillLevel = rdr.GetString(3);
                    poco.StartMonth = rdr.GetByte(4);
                    poco.StartYear = rdr.GetInt32(5);
                    poco.EndMonth = rdr.GetByte(6);
                    poco.EndYear = rdr.GetInt32(7);
                    appPocos[x] = poco;
                    x++;
                }
                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<ApplicantSkillPoco> GetList(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantSkillPoco GetSingle(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantSkillPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantSkillPoco item in items)
                {
                    cmd.CommandText = $"DELETE FROM Applicant_Skills WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                foreach (ApplicantSkillPoco AEPoco in items)
                {
                    SqlCommand cmd = new SqlCommand(@"UPDATE Applicant_Skills
                                                SET 
                                                  Applicant = @Applicant, Skill = @Skill,Skill_Level = @Skill_Level,Start_Month = @Start_Month,Start_Year = @Start_Year,End_Month = @End_Month , End_Year = @End_Year
                                                WHERE Id =@Id", conn);
                    cmd.Parameters.AddWithValue("@Id", AEPoco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", AEPoco.Applicant);
                    cmd.Parameters.AddWithValue("@Skill", AEPoco.Skill);
                    cmd.Parameters.AddWithValue("@Skill_Level", AEPoco.SkillLevel);
                    cmd.Parameters.AddWithValue("@Start_Month", AEPoco.StartMonth);
                    cmd.Parameters.AddWithValue("@Start_Year", AEPoco.StartYear);
                    cmd.Parameters.AddWithValue("@End_Month", AEPoco.EndMonth);
                    cmd.Parameters.AddWithValue("@End_Year", AEPoco.EndYear);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}

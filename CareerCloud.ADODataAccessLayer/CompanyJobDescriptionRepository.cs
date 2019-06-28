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
    public class CompanyJobDescriptionRepository : IDataRepository<CompanyJobDescriptionPoco>
    {
        public void Add(params CompanyJobDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyJobDescriptionPoco ASPoco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Jobs_Descriptions]
                                            ([Id]
                                                 ,[Job]
                                                 ,[Job_Name]
                                                 ,[Job_Descriptions])
                                    VALUES
                                            (@Id
                                            ,@Job
                                            ,@Job_Name
                                            ,@Job_Descriptions)";
                    cmd.Parameters.AddWithValue("@Id", ASPoco.Id);
                    cmd.Parameters.AddWithValue("@Job", ASPoco.Job);
                    cmd.Parameters.AddWithValue("@Job_Name", ASPoco.JobName);
                    cmd.Parameters.AddWithValue("@Job_Descriptions", ASPoco.JobDescriptions);
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

        public IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Id],[Job],[Job_Name],[Job_Descriptions],[Time_Stamp] FROM [dbo].[Company_Jobs_Descriptions]";
                conn.Open();
                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                CompanyJobDescriptionPoco[] appPocos = new CompanyJobDescriptionPoco[7000];
                while (rdr.Read())
                {
                    CompanyJobDescriptionPoco poco = new CompanyJobDescriptionPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Job = rdr.GetGuid(1);
                    poco.JobName = rdr.GetString(2);
                    poco.JobDescriptions = rdr.GetString(3);
                    poco.TimeStamp = (byte[])rdr[4];
                    appPocos[x] = poco;
                    x++;
                }
                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<CompanyJobDescriptionPoco> GetList(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobDescriptionPoco GetSingle(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyJobDescriptionPoco item in items)
                {
                    cmd.CommandText = $"DELETE FROM Company_Jobs_Descriptions WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyJobDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                foreach (CompanyJobDescriptionPoco ASPoco in items)
                {
                    SqlCommand cmd = new SqlCommand(@"UPDATE Company_Jobs_Descriptions
                                                SET 
                                                 Job =@Job,Job_Name =@Job_Name,Job_Descriptions = @Job_Descriptions
                                                WHERE Id =@Id", conn);
                    cmd.Parameters.AddWithValue("@Id", ASPoco.Id);
                    cmd.Parameters.AddWithValue("@Job", ASPoco.Job);
                    cmd.Parameters.AddWithValue("@Job_Name", ASPoco.JobName);
                    cmd.Parameters.AddWithValue("@Job_Descriptions", ASPoco.JobDescriptions);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}

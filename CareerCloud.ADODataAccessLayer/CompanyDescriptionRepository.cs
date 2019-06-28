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
    public class CompanyDescriptionRepository : IDataRepository<CompanyDescriptionPoco>
    {
        public void Add(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyDescriptionPoco CDPoco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Descriptions]
                                       ([Id],[Company],[LanguageID],[Company_Name]
                                       ,[Company_Description])
                                 VALUES
                                       (@Id,@Company,@LanguageID
                                       ,@Company_Name,@Company_Description)";
                    cmd.Parameters.AddWithValue("@Id", CDPoco.Id);
                    cmd.Parameters.AddWithValue("@Company", CDPoco.Company);
                    cmd.Parameters.AddWithValue("@LanguageID", CDPoco.LanguageId);
                    cmd.Parameters.AddWithValue("@Company_Name", CDPoco.CompanyName);
                    cmd.Parameters.AddWithValue("@Company_Description", CDPoco.CompanyDescription);
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

        public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Id],[Company],[LanguageID],[Company_Name],[Company_Description],[Time_Stamp] FROM [dbo].[Company_Descriptions]";
                conn.Open();
                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                CompanyDescriptionPoco[] appPocos = new CompanyDescriptionPoco[1000];
                while (rdr.Read())
                {
                    CompanyDescriptionPoco poco = new CompanyDescriptionPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Company = rdr.GetGuid(1);
                    poco.LanguageId = rdr.GetString(2);
                    poco.CompanyName = rdr.GetString(3);
                    poco.CompanyDescription = rdr.GetString(4);
                    poco.TimeStamp = (byte[])rdr[5];
                    appPocos[x] = poco;
                    x++;
                }
                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<CompanyDescriptionPoco> GetList(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyDescriptionPoco GetSingle(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyDescriptionPoco item in items)
                {
                    cmd.CommandText = $"DELETE FROM Company_Descriptions WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                foreach (CompanyDescriptionPoco CDPoco in items)
                {
                    SqlCommand cmd = new SqlCommand(@"UPDATE Company_Descriptions
                                                SET 
                                                  Company = @Company, LanguageID = @LanguageId,Company_Name = @Company_Name,Company_Description = @Company_Description
                                                WHERE Id =@Id", conn);
                    cmd.Parameters.AddWithValue("@Id", CDPoco.Id);
                    cmd.Parameters.AddWithValue("@Company", CDPoco.Company);
                    cmd.Parameters.AddWithValue("@LanguageID", CDPoco.LanguageId);
                    cmd.Parameters.AddWithValue("@Company_Name", CDPoco.CompanyName);
                    cmd.Parameters.AddWithValue("@Company_Description", CDPoco.CompanyDescription);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}

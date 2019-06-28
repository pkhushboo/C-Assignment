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
    public class SystemLanguageCodeRepository : IDataRepository<SystemLanguageCodePoco>
    {
        public void Add(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SystemLanguageCodePoco SLCPoco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[System_Language_Codes]
                                       ([LanguageID],[Name],[Native_Name])
                                 VALUES
                                       (@LanguageID,@Name,@Native_Name)";
                    cmd.Parameters.AddWithValue("@LanguageID", SLCPoco.LanguageID);
                    cmd.Parameters.AddWithValue("@Name", SLCPoco.Name);
                    cmd.Parameters.AddWithValue("@Native_Name", SLCPoco.NativeName);
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

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [LanguageID],[Name],[Native_Name] FROM [dbo].[System_Language_Codes]";
                conn.Open();
                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                SystemLanguageCodePoco[] appPocos = new SystemLanguageCodePoco[1000];
                while (rdr.Read())
                {
                    SystemLanguageCodePoco poco = new SystemLanguageCodePoco();
                    poco.LanguageID = rdr.GetString(0);
                    poco.Name = rdr.GetString(1);
                    poco.NativeName = rdr.GetString(2);
                    appPocos[x] = poco;
                    x++;
                }
                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemLanguageCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SystemLanguageCodePoco item in items)
                {
                    cmd.CommandText = $"DELETE FROM System_Language_Codes WHERE LanguageID = @LanguageID";
                    cmd.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                foreach (SystemLanguageCodePoco SLCPoco in items)
                {
                    SqlCommand cmd = new SqlCommand(@"UPDATE System_Language_Codes
                                                SET 
                                                  [Name] = @Name,[Native_Name] = @Native_Name
                                                WHERE LanguageID =@LanguageID", conn);
                    cmd.Parameters.AddWithValue("@LanguageID", SLCPoco.LanguageID);
                    cmd.Parameters.AddWithValue("@Name", SLCPoco.Name);
                    cmd.Parameters.AddWithValue("@Native_Name", SLCPoco.NativeName);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}

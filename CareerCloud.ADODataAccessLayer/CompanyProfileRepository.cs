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
    public class CompanyProfileRepository : IDataRepository<CompanyProfilePoco>
    {
        public void Add(params CompanyProfilePoco[] items)
        {
             using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyProfilePoco CPPoco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Profiles]
                                       ([Id],[Registration_Date],[Company_Website],[Contact_Phone],[Contact_Name],[Company_Logo])
                                 VALUES
                                       (@Id,@Registration_Date,@Company_Website, @Contact_Phone,@Contact_Name, @Company_Logo)";
                    cmd.Parameters.AddWithValue("@Id", CPPoco.Id);
                    cmd.Parameters.AddWithValue("@Registration_Date", CPPoco.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Company_Website", CPPoco.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@Contact_Phone", CPPoco.ContactPhone);
                    cmd.Parameters.AddWithValue("@Contact_Name", CPPoco.ContactName);
                    cmd.Parameters.AddWithValue("@Company_Logo", CPPoco.CompanyLogo);
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

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Id],[Registration_Date],[Company_Website],[Contact_Phone],[Contact_Name],[Company_Logo],[Time_Stamp] FROM [dbo].[Company_Profiles]";
                conn.Open();
                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                CompanyProfilePoco[] appPocos = new CompanyProfilePoco[1000];
                while (rdr.Read())
                {
                    CompanyProfilePoco poco = new CompanyProfilePoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.RegistrationDate = rdr.GetDateTime(1);
                    if (!rdr.IsDBNull(2))
                    {
                        poco.CompanyWebsite = rdr.GetString(2);
                    }
                    poco.ContactPhone = rdr.GetString(3);
                    if (!rdr.IsDBNull(4))
                    {
                        poco.ContactName = rdr.GetString(4);
                    }
                    if (!rdr.IsDBNull(5))
                    {
                        poco.CompanyLogo = (byte[])rdr[5];
                    }
                    poco.TimeStamp = (byte[])rdr[6];
                    appPocos[x] = poco;
                    x++;
                }
                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyProfilePoco item in items)
                {
                    cmd.CommandText = $"DELETE FROM Company_Profiles WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                foreach (CompanyProfilePoco CPPoco in items)
                {
                    SqlCommand cmd = new SqlCommand(@"UPDATE Company_Profiles
                                                SET 
                                                Registration_Date = @Registration_Date,[Company_Website] =@Company_Website,[Contact_Phone] = @Contact_Phone,[Contact_Name] = @Contact_Name,[Company_Logo] = @Company_Logo
                                                WHERE Id =@Id", conn);
                    cmd.Parameters.AddWithValue("@Id", CPPoco.Id);
                    cmd.Parameters.AddWithValue("@Registration_Date", CPPoco.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Company_Website", CPPoco.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@Contact_Phone", CPPoco.ContactPhone);
                    cmd.Parameters.AddWithValue("@Contact_Name", CPPoco.ContactName);
                    cmd.Parameters.AddWithValue("@Company_Logo", CPPoco.CompanyLogo);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}

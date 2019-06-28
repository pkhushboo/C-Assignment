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
    public class SecurityLoginRepository : IDataRepository<SecurityLoginPoco>
    {
        public void Add(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SecurityLoginPoco SLPoco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Security_Logins]
                                        ([Id],[Login],[Password],[Created_Date],[Password_Update_Date],[Agreement_Accepted_Date],[Is_Locked],[Is_Inactive],[Email_Address],[Phone_Number],[Full_Name],[Force_Change_Password],[Prefferred_Language])
                                   VALUES
                                        (@Id, @Login, @Password, @Created_Date, @Password_Update_Date,@Agreement_Accepted_Date, @Is_Locked, @Is_Inactive,@Email_Address, @Phone_Number, @Full_Name, @Force_Change_Password, @Prefferred_Language)";
                    cmd.Parameters.AddWithValue("@Id", SLPoco.Id);
                    cmd.Parameters.AddWithValue("@Login", SLPoco.Login);
                    cmd.Parameters.AddWithValue("@Password", SLPoco.Password);
                    cmd.Parameters.AddWithValue("@Created_Date", SLPoco.Created);
                    cmd.Parameters.AddWithValue("@Password_Update_Date", SLPoco.PasswordUpdate);
                    cmd.Parameters.AddWithValue("@Agreement_Accepted_Date", SLPoco.AgreementAccepted);
                    cmd.Parameters.AddWithValue("@Is_Locked", SLPoco.IsLocked);
                    cmd.Parameters.AddWithValue("@Is_Inactive", SLPoco.IsInactive);
                    cmd.Parameters.AddWithValue("@Email_Address", SLPoco.EmailAddress);
                    cmd.Parameters.AddWithValue("@Phone_Number", SLPoco.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Full_Name", SLPoco.FullName);
                    cmd.Parameters.AddWithValue("@Force_Change_Password", SLPoco.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@Prefferred_Language", SLPoco.PrefferredLanguage);
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

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Id],[Login],[Password],[Created_Date],[Password_Update_Date],[Agreement_Accepted_Date],[Is_Locked],[Is_Inactive],[Email_Address],[Phone_Number],[Full_Name],[Force_Change_Password],[Prefferred_Language],[Time_Stamp]  FROM [dbo].[Security_Logins]";
                conn.Open();
                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                SecurityLoginPoco[] appPocos = new SecurityLoginPoco[1000];
                while (rdr.Read())
                {
                    SecurityLoginPoco poco = new SecurityLoginPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Login = rdr.GetString(1);
                    poco.Password = rdr.GetString(2);
                    poco.Created = rdr.GetDateTime(3);
                    if (!rdr.IsDBNull(4))
                    {
                        poco.PasswordUpdate = rdr.GetDateTime(4);
                    }
                    if (!rdr.IsDBNull(5))
                    {
                        poco.AgreementAccepted = rdr.GetDateTime(5);
                    }
                    poco.IsLocked = rdr.GetBoolean(6);
                    poco.IsInactive = rdr.GetBoolean(7);
                    poco.EmailAddress = rdr.GetString(8);
                    if (!rdr.IsDBNull(9))
                    {
                        poco.PhoneNumber = rdr.GetString(9);
                    }
                    if (!rdr.IsDBNull(10))
                    {
                        poco.FullName = rdr.GetString(10);
                    }
                    poco.ForceChangePassword = rdr.GetBoolean(11);
                    if (!rdr.IsDBNull(12))
                    {
                        poco.PrefferredLanguage = rdr.GetString(12);
                    }
                    poco.TimeStamp = (byte[])rdr[13]; 
                    appPocos[x] = poco;
                    x++;
                }
                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SecurityLoginPoco item in items)
                {
                    cmd.CommandText = $"DELETE FROM Security_Logins WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                foreach (SecurityLoginPoco SLPoco in items)
                {
                    SqlCommand cmd = new SqlCommand(@"UPDATE Security_Logins
                                                SET 
                                                  Login = @Login,Password = @Password,Created_Date = @Created_Date,Password_Update_Date = @Password_Update_Date,Agreement_Accepted_Date = @Agreement_Accepted_Date,Is_Locked = @Is_Locked,Is_Inactive = @Is_Inactive,Email_Address = @Email_Address,Phone_Number = @Phone_Number,Full_Name = @Full_Name,Force_Change_Password = @Force_Change_Password,Prefferred_Language = @Prefferred_Language WHERE Id =@Id", conn);
                    cmd.Parameters.AddWithValue("@Id", SLPoco.Id);
                    cmd.Parameters.AddWithValue("@Login", SLPoco.Login);
                    cmd.Parameters.AddWithValue("@Password", SLPoco.Password);
                    cmd.Parameters.AddWithValue("@Created_Date", SLPoco.Created);
                    cmd.Parameters.AddWithValue("@Password_Update_Date", SLPoco.PasswordUpdate);
                    cmd.Parameters.AddWithValue("@Agreement_Accepted_Date", SLPoco.AgreementAccepted);
                    cmd.Parameters.AddWithValue("@Is_Locked", SLPoco.IsLocked);
                    cmd.Parameters.AddWithValue("@Is_Inactive", SLPoco.IsInactive);
                    cmd.Parameters.AddWithValue("@Email_Address", SLPoco.EmailAddress);
                    cmd.Parameters.AddWithValue("@Phone_Number", SLPoco.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Full_Name", SLPoco.FullName);
                    cmd.Parameters.AddWithValue("@Force_Change_Password", SLPoco.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@Prefferred_Language", SLPoco.PrefferredLanguage);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}

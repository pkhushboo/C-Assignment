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
    public class CompanyJobRepository : IDataRepository<CompanyJobPoco>
    {
        public void Add(params CompanyJobPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyJobPoco CJPoco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Jobs]
                                        ([Id],[Company],[Profile_Created],[Is_Inactive],[Is_Company_Hidden])
                                 VALUES
                                        (@Id,@Company,@Profile_Created,@Is_Inactive,@Is_Company_Hidden)";
                    cmd.Parameters.AddWithValue("@Id", CJPoco.Id);
                    cmd.Parameters.AddWithValue("@Company", CJPoco.Company);
                    cmd.Parameters.AddWithValue("@Profile_Created", CJPoco.ProfileCreated);
                    cmd.Parameters.AddWithValue("@Is_Inactive", CJPoco.IsInactive);
                    cmd.Parameters.AddWithValue("@Is_Company_Hidden", CJPoco.IsCompanyHidden);
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

        public IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Id],[Company],[Profile_Created],[Is_Inactive],[Is_Company_Hidden],[Time_Stamp] FROM [dbo].[Company_Jobs]";
                conn.Open();
                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                CompanyJobPoco[] appPocos = new CompanyJobPoco[7000];
                while (rdr.Read())
                {
                    CompanyJobPoco poco = new CompanyJobPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Company = rdr.GetGuid(1);
                    poco.ProfileCreated = rdr.GetDateTime(2);
                    poco.IsInactive = rdr.GetBoolean(3);
                    poco.IsCompanyHidden = rdr.GetBoolean(4);
                    poco.TimeStamp = (byte[])rdr[5];
                    appPocos[x] = poco;
                    x++;
                }
                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<CompanyJobPoco> GetList(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobPoco GetSingle(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyJobPoco item in items)
                {
                    cmd.CommandText = $"DELETE FROM Company_Jobs WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyJobPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                foreach (CompanyJobPoco CJPoco in items)
                {
                    SqlCommand cmd = new SqlCommand(@"UPDATE Company_Jobs
                                                SET 
                                                  Company = @Company,Profile_Created = @Profile_Created,Is_Inactive = @Is_Inactive ,Is_Company_Hidden = @Is_Company_Hidden
                                                WHERE Id =@Id", conn);
                    cmd.Parameters.AddWithValue("@Id", CJPoco.Id);
                    cmd.Parameters.AddWithValue("@Company", CJPoco.Company);
                    cmd.Parameters.AddWithValue("@Profile_Created", CJPoco.ProfileCreated);
                    cmd.Parameters.AddWithValue("@Is_Inactive", CJPoco.IsInactive);
                    cmd.Parameters.AddWithValue("@Is_Company_Hidden", CJPoco.IsCompanyHidden);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}

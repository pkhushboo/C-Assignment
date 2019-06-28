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
    public class CompanyLocationRepository : IDataRepository<CompanyLocationPoco>
    {
        public void Add(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyLocationPoco CLPoco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Locations]
                                        ([Id],[Company],[Country_Code],[State_Province_Code],[Street_Address],[City_Town],[Zip_Postal_Code])
                                     VALUES
                                        (@Id,@Company,@Country_Code,@State_Province_Code,@Street_Address, @City_Town,@Zip_Postal_Code)";
                    cmd.Parameters.AddWithValue("@Id", CLPoco.Id);
                    cmd.Parameters.AddWithValue("@Company", CLPoco.Company);
                    cmd.Parameters.AddWithValue("@Country_Code", CLPoco.CountryCode);
                    cmd.Parameters.AddWithValue("@State_Province_Code", CLPoco.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", CLPoco.Street);
                    cmd.Parameters.AddWithValue("@City_Town", CLPoco.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", CLPoco.PostalCode);
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

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Id],[Company],[Country_Code],[State_Province_Code],[Street_Address],[City_Town],[Zip_Postal_Code],[Time_Stamp]
                     FROM [dbo].[Company_Locations]";
                conn.Open();
                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                CompanyLocationPoco[] appPocos = new CompanyLocationPoco[1000];
                while (rdr.Read())
                {
                    CompanyLocationPoco poco = new CompanyLocationPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Company = rdr.GetGuid(1);
                    poco.CountryCode = rdr.GetString(2);
                    poco.Province = rdr.GetString(3);
                    poco.Street = rdr.GetString(4);
                    if (!rdr.IsDBNull(5))
                    {
                        poco.City = rdr.GetString(5);
                    }
                    if (!rdr.IsDBNull(6))
                    {
                        poco.PostalCode = rdr.GetString(6);
                    }
                    poco.TimeStamp = (byte[])rdr[7];
                    appPocos[x] = poco;
                    x++;
                }
                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyLocationPoco item in items)
                {
                    cmd.CommandText = $"DELETE FROM Company_Locations WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                foreach (CompanyLocationPoco CLPoco in items)
                {
                    SqlCommand cmd = new SqlCommand(@"UPDATE [dbo].[Company_Locations]
                                       SET [Company] = @Company
                                          ,[Country_Code] = @Country_Code
                                          ,[State_Province_Code] = @State_Province_Code
                                          ,[Street_Address] = @Street_Address
                                          ,[City_Town] = @City_Town
                                          ,[Zip_Postal_Code] = @Zip_Postal_Code
                                 WHERE Id = @Id", conn);
                    cmd.Parameters.AddWithValue("@Id", CLPoco.Id);
                    cmd.Parameters.AddWithValue("@Company", CLPoco.Company);
                    cmd.Parameters.AddWithValue("@Country_Code", CLPoco.CountryCode);
                    cmd.Parameters.AddWithValue("@State_Province_Code", CLPoco.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", CLPoco.Street);
                    cmd.Parameters.AddWithValue("@City_Town", CLPoco.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", CLPoco.PostalCode);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TableData.Models
{
    public class DBcon
    {
        static string ConConnect = @"Data Source=RILPT172;Initial Catalog=DressD;User ID=sa;Password=sa123";
        public List<Objects> GetDresses()
        {
            var list = new List<Objects>();
            using (SqlConnection con = new SqlConnection(ConConnect))
            {
                try
                {
                    var query = "SELECT * FROM Dresses";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var drs = new Objects();
                        drs.DressBrand = reader[0].ToString();
                        drs.DressID = Convert.ToInt32(reader[1]);
                        drs.DressType = reader[2].ToString();
                        drs.DressPrice= Convert.ToInt32(reader[3]);
                        list.Add(drs);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return list;
            }
            
        }
        public void AddDress(Objects ad)
        {
            using (SqlConnection con = new SqlConnection(ConConnect))
            {
                string query = "INSERT INTO Dresses VALUES(@DressBrand,@DressID,@DressType,@DressPrice)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DressBrand",ad.DressBrand );
                cmd.Parameters.AddWithValue("@DressID",ad.DressID );
                cmd.Parameters.AddWithValue("@DressType",ad.DressType );
                cmd.Parameters.AddWithValue("@DressPrice", ad.DressPrice);

                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new Exception("Dress is not added...!");
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public Objects FindDress(int id)
        {
            Objects drs = new Objects();
            using (SqlConnection con = new SqlConnection(ConConnect))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM Dresses WHERE DressID =  " + id;
                    SqlCommand cmd = new SqlCommand(query, con);
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        drs.DressBrand = reader[0].ToString();
                        drs.DressID = Convert.ToInt32(reader[1]);
                        drs.DressType = reader[2].ToString();
                        drs.DressPrice = Convert.ToInt32(reader[3]);
                    }
                    else
                        throw new Exception("Dress not find'''!");
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
                return drs;
            }

        }
        public void UpdateDress(Objects drs)
        {
            using (SqlConnection con = new SqlConnection(ConConnect))
            {
                var query = $"UPDATE Dresses set DressBrand = '{ drs.DressBrand }', DressID = '{drs.DressID}', DressType = '{drs.DressType}',DressPrice='{drs.DressPrice}' where DressId = {drs.DressID}";
                SqlCommand cmd = new SqlCommand(query, con);
                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new Exception("Dress is not updated");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public void DeleteDress(int id)
        {
            Objects drs = new Objects();
            using (SqlConnection con = new SqlConnection(ConConnect))
            {

                try
                {
                    con.Open();
                    var cmd = con.CreateCommand();
                    cmd.CommandText = "DELETE FROM Dresses WHERE DressID = " + id;
                    int affectedRows = cmd.ExecuteNonQuery();
                    if (affectedRows == 0)
                        throw new Exception("The selected dress is not deleted...!");
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}
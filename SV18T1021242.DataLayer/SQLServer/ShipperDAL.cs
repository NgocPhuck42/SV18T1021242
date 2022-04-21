using SV18T1021242.DomainModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021242.DataLayer.SQLServer
{
    /// <summary>
    /// 
    /// </summary>
    public class ShipperDAL : _BaseDAL, IShipperDAL 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public ShipperDAL(string connectionString) : base(connectionString)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Shipper data)
        {
            int result = 0;

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"insert into Shippers(ShipperName, Phone)
                                    values(@ShipperName, @Phone) 
                                    select scope_identity()
                                ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@ShipperName", data.ShipperName);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);


                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }

            return result;
        }

        public int Count(string searchValue)
        {
            int count = 0;
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT Count(*)
                FROM    Shippers
                WHERE    (@searchValue = N'')
                    OR    (
                            (ShipperName LIKE @searchValue)
                         OR (Phone LIKE @searchValue)
                        )";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                count = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }

            return count;
        }

        public bool Delete(int shipperID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"Delete  from Shippers where ShipperID = @shipperID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@shipperID", shipperID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }

        public Shipper Get(int shipperID)
        {
            Shipper result = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select * from Shippers where ShipperID = @shipperID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@shipperID", shipperID);
                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    result = new Shipper()
                    {
                        ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                        ShipperName = Convert.ToString(dbReader["ShipperName"]),
                        Phone = Convert.ToString(dbReader["Phone"]),
                        
                    };
                }

                cn.Close();
            }
            return result;
        }

        public bool InShipper(int shipperID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select case when exists(select * from Orders where ShipperID = @shipperID) then 1 else 0 end";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@shipperID", shipperID);
                result = Convert.ToBoolean(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }

        public IList<Shipper> List(int page, int pageSize, string searchValue)
        {
            List<Shipper> data = new List<Shipper>();
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = @"SELECT *
                    FROM
                    (
                        SELECT    *, ROW_NUMBER() OVER (ORDER BY ShipperName) AS RowNumber
                        FROM    Shippers
                        WHERE    (@searchValue = N'')
                            OR    (
                                    (ShipperName LIKE @searchValue)
                                 OR (Phone LIKE @searchValue)
                                )
                    ) AS t
                    WHERE t.RowNumber BETWEEN (@page - 1) * @pageSize + 1 AND @page * @pageSize;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                var result = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (result.Read())
                {
                    data.Add(new Shipper()
                    {
                        ShipperID = Convert.ToInt32(result["ShipperID"]),
                        ShipperName = Convert.ToString(result["ShipperName"]),
                        Phone = Convert.ToString(result["Phone"]),
                        
                    });
                }
                cn.Close();
            }

            return data;
        }

        public bool Update(Shipper data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"update Shippers set
                                    ShipperName = @ShipperName, Phone = @Phone where ShipperID = @ShipperID";
                cmd.CommandType = CommandType.Text;

                cmd.Connection = cn;


                cmd.Parameters.AddWithValue("@ShipperName", data.ShipperName);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);


                cmd.Parameters.AddWithValue("@ShipperID", data.ShipperID);
                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }
    }
}

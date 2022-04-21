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
    public class CountryDAL : _BaseDAL, ICountryDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public CountryDAL(string connectionString) : base(connectionString)
        {

        }
        public IList<Country> List()
        {
            List<Country> data = new List<Country>();
           
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = @"Select * from Countries";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
               
                var result = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (result.Read())
                {
                    data.Add(new Country()
                    {
                        CountryName = Convert.ToString(result["CountryName"])
                    });
                }
                cn.Close();
            }

            return data;
        }
    }
}

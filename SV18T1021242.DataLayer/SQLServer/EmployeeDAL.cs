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
    public class EmployeeDAL : _BaseDAL ,IEmployeeDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public EmployeeDAL(string connectionString) : base(connectionString)
        {

        }
        public int Add(Employee data)
        {
            throw new NotImplementedException();
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
                FROM    Employees
                WHERE    (@searchValue = N'')
                    OR    (
                            (LastName LIKE @searchValue)
                         OR (FirstName LIKE @searchValue)
                         OR (Email LIKE @searchValue)
                        )";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                count = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }

            return count;
        }

        public bool Delete(int employeeID)
        {
            throw new NotImplementedException();
        }

        public Employee Get(int employeeID)
        {
            throw new NotImplementedException();
        }

        public bool InUsed(int employeeID)
        {
            throw new NotImplementedException();
        }

        public IList<Employee> List(int page, int pageSize, string searchValue)
        {
            List<Employee> data = new List<Employee>();
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = @"SELECT *
                    FROM
                    (
                        SELECT    *, ROW_NUMBER() OVER (ORDER BY LastName , FirstName) AS RowNumber
                        FROM    Employees
                        WHERE    (@searchValue = N'')
                            OR    (
                                    (LastName LIKE @searchValue)
                                 OR (FirstName LIKE @searchValue)
                                 OR (Email LIKE @searchValue)
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
                    data.Add(new Employee()
                    {
                        
                        EmployeeID = Convert.ToInt32(result["EmployeeID"]),
                        LastName = Convert.ToString(result["LastName"]),
                        FirstName = Convert.ToString(result["FirstName"]),
                        BirthDate = (DateTime)(result["BirthDate"]),
                        Photo = Convert.ToString(result["Photo"]),
                        Notes = Convert.ToString(result["Notes"]),
                        Email = Convert.ToString(result["Email"])
                    });
                }
                cn.Close();
            }

            return data;
        }

        public bool Update(Employee data)
        {
            throw new NotImplementedException();
        }
    }
}

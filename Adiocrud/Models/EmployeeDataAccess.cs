using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Data;
using System.Data;

namespace Adiocrud.Models
{
    public class EmployeeDataAccess
    {
        DbConnection DbConnection;
        public EmployeeDataAccess()
        {
            DbConnection = new DbConnection();
        }
        public List<Employee> GetEmployees()
        {
            string Sp = "SP_Employees";
            SqlCommand sql = new SqlCommand(Sp,DbConnection.Connection);
            sql.CommandType = CommandType.StoredProcedure;
            sql.Parameters.AddWithValue("@action", "SELECT_JOIN");
            if (DbConnection.Connection.State == ConnectionState.Closed)
            {
                DbConnection.Connection.Open();
            }
            SqlDataReader dr = sql.ExecuteReader();
            List<Employee> employees = new List<Employee>();
            while (dr.Read())
            {
                Employee Emp = new Employee();
                Emp.Id = (int)dr["Id"];
                Emp.Name = dr["Name"].ToString();
                Emp.Email = dr["Email"].ToString();
                Emp.Gender = dr["Gender"].ToString();
                Emp.Mobile = dr["Mobile"].ToString();
      


                employees.Add(Emp);

            }
            DbConnection.Connection.Close();
            return employees;
        }
    }
}

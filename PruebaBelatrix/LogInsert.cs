using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace PruebaBelatrix
{
    class LogInsert
    {
        private string ConnectionString = "";

        public LogInsert()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
        public bool Insert(string Message, int Type)
        {
            int Result = 0;
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "LogInsert";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter Parameter = null;

            Parameter = new SqlParameter();
            Parameter.ParameterName = "@Message";
            Parameter.SqlDbType = SqlDbType.VarChar;
            Parameter.Direction = ParameterDirection.Input;
            Parameter.Value = Message;
            command.Parameters.Add(Parameter);

            Parameter = new SqlParameter();
            Parameter.ParameterName = "@Type";
            Parameter.SqlDbType = SqlDbType.Int;
            Parameter.Direction = ParameterDirection.Input;
            Parameter.Value = Type;
            command.Parameters.Add(Parameter);

            connection.Open();
            Result = command.ExecuteNonQuery();
            connection.Close();

            if (Result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
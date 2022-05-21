using MySql.Data.MySqlClient;
using System.Data;

namespace API_VNA_2._0.Data
{
    public class DataAccess
    {
        /// <summary>
        /// Access Data Base and return Clients Table
        /// </summary>
        public static DataTable GetClientTable()
        {
            // New empty DataTable
            DataTable dt = new();

            // Auxiliary variable for sql statement
            var sql_select = "SELECT * FROM cli_app.cliente";

            try
            {
                // Create new connection to mysql using connection string
                using (var connection = new MySqlConnection(Conn.strConn))
                {
                    // Create new data adapter varible to get table result of sql select statement
                    using (var data = new MySqlDataAdapter(sql_select, connection))
                    {
                        // Fill data table with content from data adapater variable
                        data.Fill(dt);

                        // Return Data Table
                        return dt;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Access Data Base and return Users Table
        /// </summary>
        public static DataTable User_list()
        {
            DataTable dt = new();
            var cmd = "SELECT username, type FROM internal.users";
            try
            {
                using (var connection = new MySqlConnection(Conn.strConn2))
                {
                    MySqlCommand cmd_aux = new MySqlCommand(cmd, connection);
                    using (var data = new MySqlDataAdapter(cmd, connection))
                    {
                        data.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Access Data Base and return Products Table
        /// </summary>
        public static DataTable GetProductsTable()
        {
            // New empty DataTable
            DataTable dt = new();

            // Auxiliary variable for sql statement
            var sql_select = "SELECT * FROM cli_app.Produtos";

            try
            {
                // Create new connection to mysql using connection string
                using (var connection = new MySqlConnection(Conn.strConn))
                {
                    // Create new data adapter varible to get table result of sql select statement
                    using (var data = new MySqlDataAdapter(sql_select, connection))
                    {
                        // Fill data table with content from data adapater variable
                        data.Fill(dt);
                    }
                }
            }
            catch (Exception)
            {
                // Return empty table
                return dt;
            }
            // Return Data Table
            return dt;
        }

        /// <summary>
        /// Access Data Base and return Sales Table
        /// </summary>
        public static DataTable GetSalesTable()
        {
            // New empty DataTable
            DataTable dt = new();

            // Auxiliary variable for sql statement
            var sql_select = "SELECT * FROM cli_app.sales_view";

            try
            {
                // Create new connection to mysql using connection string
                using (var connection = new MySqlConnection(Conn.strConn))
                {
                    // Create new data adapter varible to get table result of sql select statement
                    using (var data = new MySqlDataAdapter(sql_select, connection))
                    {
                        // Fill data table with content from data adapater variable
                        data.Fill(dt);
                    }
                }
            }
            catch (Exception)
            {
                // Return empty table
                return dt;
            }
            // Return Data Table
            return dt;
        }

        // <summary>
        /// Access Data Base and return Users
        /// </summary>
        public static int VerifyUser(string token)
        {
            var select = "SELECT token_type FROM internal.accesstoken WHERE token = @token";
            int aux = 0;
            try
            {
                using (var connection = new MySqlConnection(Conn.strConn2))
                {
                    MySqlCommand select_Aux = new MySqlCommand(select, connection);

                    select_Aux.Parameters.AddWithValue("@token", token);
                    connection.Open();

                    aux = Convert.ToInt32(select_Aux.ExecuteScalar());
                }
            }
            catch (Exception)
            {
                // Do Action
            }
            return aux;
        }

        

    }
}
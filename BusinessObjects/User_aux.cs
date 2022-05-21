using MySql.Data.MySqlClient;
using API_VNA_2._0.Data;
using System.Data;

namespace API_VNA_2._0.BusinessObjects
{
    [Serializable]
    public class User_aux
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? User_type { get; set; }

        public User_aux() { }

        public User_aux(string? username, string? password, string? type)
        {
            this.Username = username;
            this.Password = password;
            this.User_type = type;
        }

        public static List<User_aux> Get_user_list()
        {
            DataTable dt = DataAccess.User_list();
            List<User_aux> list = new();

            string? username;
            string? password;
            string? type;

            list = (from DataRow dr in dt.Rows select new User_aux(
                username = dr["username"].ToString(),
                password = "",
                type = dr["type"].ToString()
                )).ToList();
            return list;

        }

        public static bool Login(User_aux u)
        {
            if(u.Username != "" && u.Password != "")
            {
                var cmd = "SELECT COUNT(id) FROM internal.users WHERE username = @user AND password = sha1(@aux)";

                try
                {
                    using(var connection = new MySqlConnection(Conn.strConn2))
                    {
                        MySqlCommand cmd_aux = new MySqlCommand(cmd, connection);
                        cmd_aux.Parameters.AddWithValue("@user", u.Username);
                        cmd_aux.Parameters.AddWithValue("@aux", u.Password);

                        connection.Open();
                        int resp = Convert.ToInt32(cmd_aux.ExecuteScalar());
                        if (resp == 1) return true;
                        else return false;
                    }
                }catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else { return false; }
        }

        public static int Total_u(int opt)
        {

            var cmd = "";
            if(opt == 0) cmd = "SELECT COUNT(id) FROM internal.users";
            else if(opt == 1) cmd = "SELECT COUNT(id) FROM internal.users WHERE type = 'admin'";
            try
            {
                using(var connection = new MySqlConnection(Conn.strConn2))
                {
                    MySqlCommand cmd_aux = new MySqlCommand(cmd, connection);
                    connection.Open();
                    int resp = Convert.ToInt32(cmd_aux.ExecuteScalar());
                    return resp;
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string? Get_last()
        {
            var cmd = "SELECT username FROM internal.users ORDER BY id DESC LIMIT 1";
            try
            {
                using(var connection = new MySqlConnection(Conn.strConn2))
                {
                    MySqlCommand cmd_aux = new MySqlCommand(cmd, connection);
                    connection.Open();
                    string? resp = cmd_aux.ExecuteScalar().ToString();
                    return resp;
                }
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

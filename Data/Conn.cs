
namespace API_VNA_2._0.Data
{
    public class Conn
    {
        // Variáveis Auxiliares para utilização dos dados de acesso ao servidor
        private const string servidor = "myvnadb.mysql.database.azure.com";
        private const string port = "3306";
        private const string schemaCli_App = "cli_app";
        private const string schemaUser = "internal";
        private const string usuario = "jynx";
        private const string senha = "";

        static public string strConn = $"server={servidor};port={port};User Id={usuario};database={schemaCli_App};password={senha}";
        static public string strConn2 = $"server={servidor};port={port};User Id={usuario};database={schemaUser};password={senha}";
    }
}


namespace API_VNA_2._0.Data
{
    public class Conn
    {
        // Variáveis Auxiliares para utilização dos dados de acesso ao servidor
        private const string servidor = "";
        private const string port = "";
        private const string schemaCli_App = "";
        private const string schemaUser = "";
        private const string usuario = "";
        private const string senha = "";

        static public string strConn = $"server={servidor};port={port};User Id={usuario};database={schemaCli_App};password={senha}";
        static public string strConn2 = $"server={servidor};port={port};User Id={usuario};database={schemaUser};password={senha}";
    }
}


namespace API_VNA_2._0.Data
{
    public class Conn
    {
        // Variáveis Auxiliares para utilização dos dados de acesso ao servidor
        private const string servidor = "204.2.63.87";
        private const string port = "10295";
        private const string bancoDados = "cli_app";
        private const string usuario = "admin";
        private const string senha = "hummerh14x4rally";

        static public string strConn = $"server={servidor};port={port};User Id={usuario};database={bancoDados};password={senha}";
    }
}

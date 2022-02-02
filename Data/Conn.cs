
namespace API_VNA_2._0.Data
{
    public class Conn
    {
        // Variáveis Auxiliares para utilização dos dados de acesso ao servidor
        private const string servidor = "example";
        private const string port = "example";
        private const string bancoDados = "example";
        private const string usuario = "example";
        private const string senha = "example";

        static public string strConn = $"server={servidor};port={port};User Id={usuario};database={bancoDados};password={senha}";
    }
}


namespace API_VNA_2._0.Data
{
    public class Conn
    {
        // Variáveis Auxiliares para utilização dos dados de acesso ao servidor
        private const string servidor = "EXAMPLE";
        private const string port = "EXAMPLE";
        private const string bancoDados = "EXAMPLE";
        private const string usuario = "EXAMPLE";
        private const string senha = "EXAMPLE";

        static public string strConn = $"server={servidor};port={port};User Id={usuario};database={bancoDados};password={senha}";
    }
}

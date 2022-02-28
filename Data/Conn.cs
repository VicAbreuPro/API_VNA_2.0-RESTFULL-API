
namespace API_VNA_2._0.Data
{
    public class Conn
    {
        // Variáveis Auxiliares para utilização dos dados de acesso ao servidor
        private const string servidor = "";
        private const string port = "";
        private const string bancoDados = "";
        private const string usuario = "";
        private const string senha = "";

        static public string strConn = $"server={servidor};port={port};User Id={usuario};database={bancoDados};password={senha}";
    }
}

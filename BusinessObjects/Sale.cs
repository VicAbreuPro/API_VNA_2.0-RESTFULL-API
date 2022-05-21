using API_VNA_2._0.Data;
using System.Data;
using MySql.Data.MySqlClient;

namespace API_VNA_2._0.BusinessObjects
{
    /// <summary>
    /// Serializable object Business Sale
    /// </summary>
    [Serializable]
    public class Sale
    {
        public string ?sale_id { get; set; }
        public int ?serial { get; set; }
        public int valor { get; set; }
        public string ?model { get; set; }
        public int ?client_id { get; set; }
        public string? date { get; set; }

        public Sale()
        {
            // Class Deafult Constructor
        }

        public Sale(string sale_id, int client_id, int serial, string model, string date, int valor)
        {
            this.sale_id = sale_id;
            this.serial = serial;
            this.model = model;
            this.client_id = client_id;
            this.date = date;
            this.valor = valor;
        }

        /// <summary>
        /// Obter Lista de Vendas da Base de Dados
        /// </summary>
        public static List<Sale> GetSales()
        {
            DataTable dt = DataAccess.GetSalesTable();
            List<Sale> p = new();

            string? sale_id;
            int client_id;
            int serial;
            string? model;
            int valor;
            string? date;

            // LINQ
            p = (from DataRow dr in dt.Rows select new Sale(sale_id = dr["saleID"].ToString(), client_id = Convert.ToInt32(dr["client_id"]), serial = Convert.ToInt32(dr["serial_number"]), model = dr["model"].ToString(), date = dr["data_buy"].ToString(), valor = Convert.ToInt32(dr["valor"]))).ToList();

            // Return list of sales
            return p;
        }

        public static bool AddSale(Sale s)
        {
            // Atribuir a uma variável o comando SQL para inserir os dados e seus respetivos valores
            var sqlInsert = "INSERT INTO cli_app.Sales(serial_number, model, data_buy, client_id, valor) VALUES(@serial, @model, @data_buy, @client_id, @value)";

            // Iniciar processo de conexão a ao servidor com auxilio da Classe Conn que possui os dados de conexão
            try
            {
                using (var connection = new MySqlConnection(Conn.strConn))
                {
                    // Criar novo objeto "comando" para executar comando SQL criado
                    MySqlCommand sqlInsert_Aux = new MySqlCommand(sqlInsert, connection);

                    //Adicionar parâmetros ao comando de acordo com as variáveis de entrada (método mais seguro)
                    sqlInsert_Aux.Parameters.AddWithValue("@serial", s.serial);
                    sqlInsert_Aux.Parameters.AddWithValue("@model", s.model);
                    sqlInsert_Aux.Parameters.AddWithValue("@data_buy", s.date);
                    sqlInsert_Aux.Parameters.AddWithValue("@client_id", s.client_id);
                    sqlInsert_Aux.Parameters.AddWithValue("@value", s.valor);

                    // Abrir conexão com base de dados
                    connection.Open();

                    // Executar comando SQL
                    sqlInsert_Aux.ExecuteReader();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
                // Exibir mensagem do erro específico
            }
        }
        public static bool UpdateSale(Sale s)
        {
            List<Sale> saleListAux = GetSales();
            bool aux = false;

            foreach (Sale sale in saleListAux)
            {
                if (sale.sale_id == s.sale_id) aux = true;
            }

            // Se a Venda existir, atulizar as informações com os atributos do novo objeto do tipo Venda recebido
            if (aux == true)
            {
                var sqlUpdate = "UPDATE cli_app.sales SET serial_number = @serialAux , model = @modelAux , data_buy = @dateAux , client_id = @cliAux, valor = @valueAux WHERE saleID = @idAux";

                // Iniciar processo de conexão a ao servidor com auxilio da Classe Conn que possui os dados de conexão
                try
                {
                    using (var connection = new MySqlConnection(Conn.strConn))
                    {

                        // Criar novo objeto "comando" para executar comando SQL criado
                        MySqlCommand sqlInsert_Aux = new MySqlCommand(sqlUpdate, connection);

                        //Adicionar parâmetros ao comando de acordo com as variáveis de entrada (método mais seguro)
                        sqlInsert_Aux.Parameters.AddWithValue("@serialAux", s.serial);
                        sqlInsert_Aux.Parameters.AddWithValue("@modelAux", s.model);
                        sqlInsert_Aux.Parameters.AddWithValue("@dateAux", s.date);
                        sqlInsert_Aux.Parameters.AddWithValue("@cliAux", s.client_id);
                        sqlInsert_Aux.Parameters.AddWithValue("@valueAux", s.valor);
                        sqlInsert_Aux.Parameters.AddWithValue("@idAux", s.sale_id);

                        // Abrir conexão com base de dados
                        connection.Open();

                        // Executar comando SQL
                        sqlInsert_Aux.ExecuteReader();
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                    // Exibir mensagem do erro específico
                }
            }
            else return false;
        }
    }
}

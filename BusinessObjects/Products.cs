using API_VNA_2._0.Data;
using System.Data;
using MySql.Data.MySqlClient;

namespace API_VNA_2._0.BusinessObjects
{
    /// <summary>
    /// Serializable object Business Products
    /// </summary>
    [Serializable]
    public class Products
    {
        /// <summary>
        /// Obter Lista de Produtos da Base de Dados
        /// </summary>
        public static List<Product> GetProducts()
        {
            DataTable dt = DataAccess.GetProductsTable();
            List<Product> p = new();

            int serial;
            string? model;
            int valor;

            // LINQ
            p = (from DataRow dr in dt.Rows select new Product(serial = Convert.ToInt32(dr["serial_number"]), model = dr["model"].ToString(), valor = Convert.ToInt32(dr["valor"]))).ToList();

            // Return list of sales
            return p;
        }
        public static bool AddProduct(Product p)
        {
            // Atribuir a uma variável o comando SQL para inserir os dados e seus respetivos valores
            var sqlInsert = "INSERT INTO cli_app.Produtos(serial_number, model, valor) VALUES(@serial, @model, @value)";

            // Iniciar processo de conexão a ao servidor com auxilio da Classe Conn que possui os dados de conexão
            try
            {
                using (var connection = new MySqlConnection(Conn.strConn))
                {
                    // Criar novo objeto "comando" para executar comando SQL criado
                    MySqlCommand sqlInsert_Aux = new MySqlCommand(sqlInsert, connection);

                    //Adicionar parâmetros ao comando de acordo com as variáveis de entrada (método mais seguro)
                    sqlInsert_Aux.Parameters.AddWithValue("@serial", p.serial);
                    sqlInsert_Aux.Parameters.AddWithValue("@model", p.model);
                    sqlInsert_Aux.Parameters.AddWithValue("@value", p.valor);

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

        public static bool UpdateProduct(Product p)
        {
            // Variáveis Auxiliares
            bool aux = false;
            List<Product> productListAux = GetProducts();

            // Verificar se Produto existe na base de dados
            foreach (Product product in productListAux)
            {
                if (product.serial == p.serial)
                {
                    aux = true;
                }
            }

            // Se Produto existir, atulizar as informações com os atributos do novo objeto do tipo Produto recebido
            if (aux == true)
            {
                var sqlUpdate = "UPDATE cli_app.produtos SET model = @modelAux , valor = @valueAux WHERE serial_number = @serialAux";

                // Iniciar processo de conexão a ao servidor com auxilio da Classe Conn que possui os dados de conexão
                try
                {
                    using (var connection = new MySqlConnection(Conn.strConn))
                    {
                        // Criar novo objeto "comando" para executar comando SQL criado
                        MySqlCommand sqlInsert_Aux = new MySqlCommand(sqlUpdate, connection);

                        //Adicionar parâmetros ao comando de acordo com as variáveis de entrada (método mais seguro)
                        sqlInsert_Aux.Parameters.AddWithValue("@serialAux", p.serial);
                        sqlInsert_Aux.Parameters.AddWithValue("@modelAux", p.model);
                        sqlInsert_Aux.Parameters.AddWithValue("@valueAux", p.valor);

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

        public static string? TopProductStock()
        {
            string? topProduct = "";

            List<Product> productList = GetProducts();

            int counter = 0;
            List<TopAux> listAux = new();

            // Percorrer lista de produtos e adiconar cada modelo em uma nova lista
            for (int i = 0; i < productList.Count; i++)
            {
                // Variaveis auxiliares para cada ciclo
                int aux = 0;

                // Criar novo objeto tipo produto auxiliar
                TopAux tp_p = new();

                // Atribuir localização do cliente atual a um novo objeto localização
                tp_p.Name = productList[i].model;

                // Adicionar um valor a frequencia de presença do produto atual
                tp_p.Frequency = 1;

                // Verificar se é o primeiro elemento da pesquisa
                if (i == 0)
                {
                    // Adicionar novo modelo a lista auxiliar
                    listAux.Add(tp_p);
                }
                else
                {
                    // Percorrer lista auxiliar
                    foreach (var p_aux in listAux)
                    {
                        // Verificar os modelos dos produtos atuais com os produtos existentes na lista principal
                        if (p_aux.Name == productList[i].model)
                        {
                            // Caso o modelo se repita, a sua frequência de presença é atualizada
                            p_aux.Frequency++;

                            //Verificador auxiliar para adição de um novo produto
                            aux = 1;
                        }
                    }
                    // Caso o modelo nao seja igual aos produtos ja contidos na lista auxiliar, entao adiconar esse novo produto
                    if (aux != 1) listAux.Add(tp_p);
                }
            }

            // Percorrer a lista auxiliar de produtos e verificar qual a frequência mais alta
            for (int i = 0; i < listAux.Count; i++)
            {
                // Adicionar um valor ao contador no primeiro produto analisado
                if (i == 0)
                {
                    counter = listAux[i].Frequency;
                    topProduct = listAux[i].Name;
                }

                // Se nao for o primeiro item da lista e a frequência for maior que o valor do contador, então atualizar novo valor do mesmo
                if (i != 0 && listAux[i].Frequency > counter)
                {
                    counter = listAux[i].Frequency;
                    topProduct = listAux[i].Name;
                }
            }
            return topProduct;
        }
    }
}

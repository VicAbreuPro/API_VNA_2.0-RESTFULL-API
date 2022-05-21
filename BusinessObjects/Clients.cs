using API_VNA_2._0.Data;
using System.Data;
using MySql.Data.MySqlClient;

namespace API_VNA_2._0.BusinessObjects
{
    /// <summary>
    /// Serializable object Business Clients
    /// </summary>
    [Serializable]
    public class Clients
    {
        #region Metodos
        /// <summary>
        /// Retornar List De Clientes
        /// </summary>
        public static List<Client> GetClients()
        {
            DataTable dt = DataAccess.GetClientTable();
            List<Client> c = new();

            string? id;
            string? name;
            string? location;
            string? date;

            c = (from DataRow dr in dt.Rows select new Client(id = dr["client_id"].ToString(), name = dr["client_name"].ToString(), location = dr["client_location"].ToString(), date = dr["since_date"].ToString())).ToList();
            return c;
        }

        /// <summary>
        /// Adicionar Cliente a base de dados
        /// </summary>
        /// <param name="c"<></param>
        public static bool AddClient(Client c)
        {
            // Atribuir a uma variável o comando SQL para inserir os dados e seus respetivos valores
            var sqlInsert = "INSERT INTO cli_app.Cliente(client_name, client_location, since_date) VALUES(@name, @local, @data)";

            // Iniciar processo de conexão a ao servidor com auxilio da Classe Conn que possui os dados de conexão
            try
            {
                using (var connection = new MySqlConnection(Conn.strConn))
                {
                    // Criar novo objeto "comando" para executar comando SQL criado
                    MySqlCommand sqlInsert_Aux = new MySqlCommand(sqlInsert, connection);

                    //Adicionar parâmetros ao comando de acordo com as variáveis de entrada (método mais seguro)
                    sqlInsert_Aux.Parameters.AddWithValue("@name", c.name);
                    sqlInsert_Aux.Parameters.AddWithValue("@local", c.location);
                    sqlInsert_Aux.Parameters.AddWithValue("@data", c.date);

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

        public static bool UpdateClient(Client c)
        {
            // Variáveis Auxiliares
            bool aux = false;
            List<Client> clientListAux = GetClients();

            // Verificar se Cliente existe na base de dados
            foreach (Client client in clientListAux)
            {
                if (client.id == c.id)
                {
                    aux = true;
                }
            }

            // Se cliente existir, atulizar as informações com os atributos do novo objeto do tipo Cliente recebido
            if (aux == true)
            {
                var sqlUpdate = "UPDATE cli_app.Cliente SET client_name = @cliAux , client_location = @localAux , since_date = @dateAux WHERE client_id = @idAux";

                // Iniciar processo de conexão a ao servidor com auxilio da Classe Conn que possui os dados de conexão
                try
                {
                    using (var connection = new MySqlConnection(Conn.strConn))
                    {
                        // Criar novo objeto "comando" para executar comando SQL criado
                        MySqlCommand sqlInsert_Aux = new MySqlCommand(sqlUpdate, connection);

                        //Adicionar parâmetros ao comando de acordo com as variáveis de entrada (método mais seguro)
                        sqlInsert_Aux.Parameters.AddWithValue("@cliAux", c.name);
                        sqlInsert_Aux.Parameters.AddWithValue("@localAux", c.location);
                        sqlInsert_Aux.Parameters.AddWithValue("@dateAux", c.date);
                        sqlInsert_Aux.Parameters.AddWithValue("@idAux", c.id);

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

        public static string? TopClientLocation()
        {
            List<Client> clientList = GetClients();

            // Variaveis Axuliares
            string? topLocal = "";
            int counter = 0;
            List<TopAux> locationList = new();

            // Percorrer lista de clientes e adiconar cada localização em uma nova lista
            for (int i = 0; i < clientList.Count; i++)
            {
                // Variaveis auxliares para cada ciclo
                int aux = 0;

                // Criar novo objeto tipo localização
                TopAux local = new();

                // Atribuir localização do cliente atual a um novo objeto localização
                local.Name = clientList[i].location;

                // Adicionar um valor a frequencia de aparência da presente localização
                local.Frequency = 1;

                // Verificar se é o primeiro elemento da pesquisa
                if (i == 0)
                {
                    // Adicionar nova localização a lista de localizações
                    locationList.Add(local);
                }
                else
                {
                    // Percorrer lista de Localizações
                    foreach (var local_aux in locationList)
                    {
                        // Verificar as localização do cliente atual com as localizações existentes no registos das mesmas
                        if (local_aux.Name == clientList[i].location)
                        {
                            // Caso a localização se repita, a sua frequência é atualizada
                            local_aux.Frequency++;

                            //Verificador auxiliar para adição de nova localização
                            aux = 1;
                        }
                    }
                    // Caso a localização atual nao seja igual a nenhuma contida na lista das mesmas, então adiconar nova localização
                    if (aux != 1) locationList.Add(local);
                }
            }

            // Percorrer a lista de localizações e verificar qual a frequência mais alta
            for (int i = 0; i < locationList.Count; i++)
            {
                // Adicionar um valor ao contador na primeira localização analisada
                if (i == 0)
                {
                    counter = locationList[i].Frequency;
                    topLocal = locationList[i].Name;
                }

                // Se nao for o primeiro item da lista e a frequência for maior que o valor do contador, então atualizar novo valor do mesmo
                if (i != 0 && locationList[i].Frequency > counter)
                {
                    counter = locationList[i].Frequency;
                    topLocal = locationList[i].Name;
                }
            }
            return topLocal;
        }
        #endregion
    }
}


using System.Data;

namespace API_VNA_2._0.BusinessObjects
{
    /// <summary>
    /// Serializable object Business Clients
    /// </summary>
    [Serializable]
    public class Clients
    {
        public static List<Client> clientList = new();

        public static List<Client> ClientList
        {
            get { return clientList; }
        }

        public static string TopClientLocation()
        {
            clientList = Data.DataAccess.GetClients();

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
    }
}

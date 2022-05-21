using System.Data;

namespace API_VNA_2._0.BusinessObjects
{
    /// <summary>
    /// Serializable object Business Products
    /// </summary>
    [Serializable]
    public class Products
    {
        public static List<Product> productList = new();

        public static List<Product> ProductList
        {
            get { return productList; }
        }

        public static string TopProductStock()
        {
            string? topProduct = "";

            productList = Data.DataAccess.GetProducts();

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

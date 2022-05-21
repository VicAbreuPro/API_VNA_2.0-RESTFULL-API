using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_VNA_2._0.BusinessObjects
{
    /// <summary>
    /// Serializable object Business Sales
    /// </summary>
    [Serializable]
    public class Sales
    {
        public static List<Sale> saleList = new();

        public static List<Sale> SaleList
        {
            get { return saleList; }
        }

        public static string TopSalesProduct()
        {
            saleList = Data.DataAccess.GetSales();

            // Variaveis Axuliares
            string? topSale = "";
            int counter = 0;
            List<TopAux> listAux = new();

            // Percorrer lista de clientes e adiconar cada localização em uma nova lista
            for (int i = 0; i < saleList.Count; i++)
            {
                // Variaveis auxliares para cada ciclo
                int aux = 0;

                // Criar novo objeto tipo localização
                TopAux saleAux = new();

                // Atribuir localização do cliente atual a um novo objeto localização
                saleAux.Name = saleList[i].model;

                // Adicionar um valor a frequencia de aparência da presente localização
                saleAux.Frequency = 1;

                // Verificar se é o primeiro elemento da pesquisa
                if (i == 0)
                {
                    // Adicionar nova localização a lista de localizações
                    listAux.Add(saleAux);
                }
                else
                {
                    // Percorrer lista de Localizações
                    foreach (var sale_aux in listAux)
                    {
                        // Verificar as localização do cliente atual com as localizações existentes no registos das mesmas
                        if (sale_aux.Name == saleList[i].model)
                        {
                            // Caso a localização se repita, a sua frequência é atualizada
                            sale_aux.Frequency++;

                            //Verificador auxiliar para adição de nova localização
                            aux = 1;
                        }
                    }
                    // Caso a localização atual nao seja igual a nenhuma contida na lista das mesmas, então adiconar nova localização
                    if (aux != 1) listAux.Add(saleAux);
                }
            }

            // Percorrer a lista de localizações e verificar qual a frequência mais alta
            for (int i = 0; i < listAux.Count; i++)
            {
                // Adicionar um valor ao contador na primeira localização analisada
                if (i == 0)
                {
                    counter = listAux[i].Frequency;
                    topSale = listAux[i].Name;
                }

                // Se nao for o primeiro item da lista e a frequência for maior que o valor do contador, então atualizar novo valor do mesmo
                if (i != 0 && listAux[i].Frequency > counter)
                {
                    counter = listAux[i].Frequency;
                    topSale = listAux[i].Name;
                }
            }
            return topSale;
        }

        // Get sales amount from current year in sale list
        public static int YearlySale()
        {
            // Get List of Sales
            saleList = Data.DataAccess.GetSales();

            // Get current date
            DateTime now = DateTime.Now;

            // Get current year
            string year = now.Year.ToString();

            // Auxiliary variable for sales amount
            int saleAmount = 0;

            foreach(var sale in saleList)
            {
                // Sum sales amount if sale year match with current year
                if(year == sale.date[..4])
                {
                    saleAmount +=  sale.valor;
                }
            }
            // Return year sale amount
            return saleAmount;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Data;
using API_VNA_2._0.BusinessObjects;
using System.Threading.Tasks;

namespace API_VNA_2._0.Data
{
    public class DataAccess
    {
        public static DataSet AllData()
        {
            DataSet dt = new DataSet();

            dt = AllDataAux(dt);

            return dt;
        }

        /// <summary>
        /// Retornar todos os Clientes e Produtos da base de dados
        /// </summary>
        /// <param name="dt"<></param>
        public static DataSet AllDataAux(DataSet dt)
        {
            // Criar nova variavel para comando SQL
            var sql_sales = "SELECT * FROM cli_app.Sales";
            var sql_c = "SELECT * FROM cli_app.Cliente";
            var sql_p = "SELECT * FROM cli_app.Produtos";
            var sql_P_Detail = "SELECT * FROM cli_app.productdetail";

            // Conectar com base de Dados configurada na Clase Conn
            try
            {
                // Abrir conexão com variavel Conn
                using (var connection = new MySqlConnection(Conn.strConn))
                {
                    // Data Adapater para trazer os dados da DB
                    connection.Open();
                    using (var dataN = new MySqlDataAdapter(sql_sales, connection)) // MySqlDataAdapater("command","MySqlConnection)
                    {
                        //Preencher os dados de dataN na variável previamente definida "dt"
                        dataN.Fill(dt, "vendas");
                    }

                    using (var dataN_Aux = new MySqlDataAdapter(sql_c, connection))
                    {
                        dataN_Aux.Fill(dt, "clientes");
                    }

                    using (var dataN_Aux_1 = new MySqlDataAdapter(sql_p, connection))
                    {
                        dataN_Aux_1.Fill(dt, "produtos");
                    }

                    using (var dataN_Aux_2 = new MySqlDataAdapter(sql_P_Detail, connection))
                    {
                        dataN_Aux_2.Fill(dt, "p_detail");
                    }
                }
            }
            catch (Exception)
            {
                // Exibir mensagem do erro específico
                // Dúvida : Colocar MessageBox com erro
            }
            return dt;
        }

        /// <summary>
        /// Retornar List De Clientes
        /// </summary>
        /// <param name="c" name="dt"<></param>
        public static List<Client> GetClients(List<Client> c, DataTable dt)
        {
            string id;
            string name;
            string location;
            string date;

            c = (from DataRow dr in dt.Rows select new Client(id = dr["client_id"].ToString(), name = dr["client_name"].ToString(), location = dr["client_location"].ToString(), date = dr["since_date"].ToString())).ToList();

            return c;
        }

        /// <summary>
        /// Obter Lista de Vendas da Base de Dados
        /// </summary>
        /// <param name="p" name="dt"<></param>
        public static List<Sale> GetSales(List<Sale> p, DataTable dt)
        {
            string sale_id;
            int client_id;
            int serial;
            string model;
            int valor;
            string date;

            // LINQ
            p = (from DataRow dr in dt.Rows select new Sale(sale_id = dr["saleID"].ToString(), client_id = Convert.ToInt32(dr["client_id"]), serial = Convert.ToInt32(dr["serial_number"]), model = dr["model"].ToString(), date = dr["data_buy"].ToString(), valor = Convert.ToInt32(dr["valor"]))).ToList();

            // Return list of sales
            return p;
        }

        /// <summary>
        /// Obter Lista de Produtos da Base de Dados
        /// </summary>
        /// <param name="p" name="dt"<></param>
        public static List<Product> GetProducts(List<Product> p, DataTable dt)
        {
            int serial;
            string model;
            int valor;

            // LINQ
            p = (from DataRow dr in dt.Rows select new Product(serial = Convert.ToInt32(dr["serial_number"]), model = dr["model"].ToString(), valor = Convert.ToInt32(dr["valor"]))).ToList();

            // Return list of sales
            return p;
        }

        /// <summary>
        /// Obter Lista de Produtos da Base de Dados
        /// </summary>
        /// <param name="p" name="dt"<></param>
        public static List<ProductDetail> GetProductsDetails(List<ProductDetail> p, DataTable dt)
        {
            int weight;
            int height;
            int year;
            string model;
            string color;

            // LINQ
            p = (from DataRow dr in dt.Rows select new ProductDetail(model = dr["model"].ToString(), weight = Convert.ToInt32(dr["weight"]), height = Convert.ToInt32(dr["height"]), year = Convert.ToInt32(dr["yearFab"]), color = dr["color"].ToString())).ToList();

            // Return list of sales
            return p;
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

        public static bool VerifyUser(string name, string password)
        {
            DataSet dt = new DataSet();

            var sqlSelect = "SELECT * from cli_app.user";

            // Conectar com base de Dados configurada na Clase Conn
            try
            {
                // Abrir conexão com variavel Conn
                using (var connection = new MySqlConnection(Conn.strConn))
                {
                    // Data Adapater para trazer os dados da DB
                    connection.Open();
                    using (var dataN = new MySqlDataAdapter(sqlSelect, connection)) // MySqlDataAdapater("command","MySqlConnection)
                    {
                        //Preencher os dados de dataN na variável previamente definida "dt"
                        dataN.Fill(dt, "users");
                    }

                }
            }
            catch (Exception)
            {
                // Exibir mensagem do erro específico
                // Dúvida : Colocar MessageBox com erro
            }
            return true;
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
    }
}

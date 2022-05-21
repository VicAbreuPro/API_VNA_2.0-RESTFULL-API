﻿using MySql.Data.MySqlClient;
using System.Data;
using API_VNA_2._0.BusinessObjects;

namespace API_VNA_2._0.Data
{
    public class DataAccess
    {
        #region GET DATA TABLES FROM DB

        /// <summary>
        /// Access Data Base and return Clients Table
        /// </summary>
        public static DataTable GetClientTable()
        {
            // New empty DataTable
            DataTable dt = new();

            // Auxiliary variable for sql statement
            var sql_select = "SELECT * FROM cli_app.Cliente";

            try
            {
                // Create new connection to mysql using connection string
                using (var connection = new MySqlConnection(Conn.strConn))
                {
                    // Create new data adapter varible to get table result of sql select statement
                    using (var data = new MySqlDataAdapter(sql_select, connection))
                    {
                        // Fill data table with content from data adapater variable
                        data.Fill(dt);
                    }
                }
            }
            catch (Exception)
            {
                // Return empty table
                return dt;
            }
            // Return Data Table
            return dt;
        }

        /// <summary>
        /// Access Data Base and return Products Table
        /// </summary>
        public static DataTable GetProductsTable()
        {
            // New empty DataTable
            DataTable dt = new();

            // Auxiliary variable for sql statement
            var sql_select = "SELECT * FROM cli_app.Produtos";

            try
            {
                // Create new connection to mysql using connection string
                using (var connection = new MySqlConnection(Conn.strConn))
                {
                    // Create new data adapter varible to get table result of sql select statement
                    using (var data = new MySqlDataAdapter(sql_select, connection))
                    {
                        // Fill data table with content from data adapater variable
                        data.Fill(dt);
                    }
                }
            }
            catch (Exception)
            {
                // Return empty table
                return dt;
            }
            // Return Data Table
            return dt;
        }

        /// <summary>
        /// Access Data Base and return Sales Table
        /// </summary>
        public static DataTable GetSalesTable()
        {
            // New empty DataTable
            DataTable dt = new();

            // Auxiliary variable for sql statement
            var sql_select = "SELECT * FROM cli_app.Sales";

            try
            {
                // Create new connection to mysql using connection string
                using (var connection = new MySqlConnection(Conn.strConn))
                {
                    // Create new data adapter varible to get table result of sql select statement
                    using (var data = new MySqlDataAdapter(sql_select, connection))
                    {
                        // Fill data table with content from data adapater variable
                        data.Fill(dt);
                    }
                }
            }
            catch (Exception)
            {
                // Return empty table
                return dt;
            }
            // Return Data Table
            return dt;
        }

        
        #endregion

        #region GET DATA LIST

        /// <summary>
        /// Retornar List De Clientes
        /// </summary>
        public static List<Client> GetClients()
        {
            DataTable dt = GetClientTable();
            List<Client> c = new();

            string? id;
            string? name;
            string? location;
            string? date;

            c = (from DataRow dr in dt.Rows select new Client(id = dr["client_id"].ToString(), name = dr["client_name"].ToString(), location = dr["client_location"].ToString(), date = dr["since_date"].ToString())).ToList();
            return c;
        }



        /// <summary>
        /// Obter Lista de Vendas da Base de Dados
        /// </summary>
        public static List<Sale> GetSales()
        {
            DataTable dt = GetSalesTable();
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

        /// <summary>
        /// Obter Lista de Produtos da Base de Dados
        /// </summary>
        public static List<Product> GetProducts()
        {
            DataTable dt = GetProductsTable();
            List<Product> p = new();

            int serial;
            string? model;
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
            string? model;
            string? color;

            // LINQ
            p = (from DataRow dr in dt.Rows select new ProductDetail(model = dr["model"].ToString(), weight = Convert.ToInt32(dr["weight"]), height = Convert.ToInt32(dr["height"]), year = Convert.ToInt32(dr["yearFab"]), color = dr["color"].ToString())).ToList();

            // Return list of sales
            return p;
        }

        public static List<User_tk> Users(DataTable dt)
        {
            List<User_tk> userList = new();

            string? id;
            int type;
            string? token;

            // LINQ
            userList = (from DataRow dr in dt.Rows select new User_tk(id = dr["token_id"].ToString(), type = Convert.ToInt32(dr["token_type"]), token = dr["token"].ToString())).ToList();

            return userList;
        }
        #endregion

        #region ADD DATA
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

        public static bool AddImage(Image img)
        {
            var sqlInsert = "INSERT into cli_app.images(imgName, img) VALUES(@imgname, @imgdata)";

            try
            {
                using (var connection = new MySqlConnection(Conn.strConn))
                {
                    MySqlCommand sqlInsertAux = new MySqlCommand(sqlInsert, connection);

                    sqlInsertAux.Parameters.AddWithValue("@imgname", img.name);
                    sqlInsertAux.Parameters.AddWithValue("@imgdata", img.data);

                    connection.Open();

                    sqlInsertAux.ExecuteReader();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region UPDATE DATA
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

        public static bool UpdateSale(Sale s)
        {
            List<Sale> saleListAux = GetSales();
            bool aux = false;

            foreach(Sale sale in saleListAux)
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
                    using (var connection = new MySqlConnection(Conn.strConn)) {

                        // Criar novo objeto "comando" para executar comando SQL criado
                        MySqlCommand sqlInsert_Aux = new MySqlCommand(sqlUpdate, connection);

                        //Adicionar parâmetros ao comando de acordo com as variáveis de entrada (método mais seguro)
                        sqlInsert_Aux.Parameters.AddWithValue("@serialAux", s.serial);
                        sqlInsert_Aux.Parameters.AddWithValue("@modelAux",s.model);
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

        #endregion

        #region INTERNAL AUX FUNCTIONS

        /// <summary>
        /// Access Data Base and return Users
        /// </summary>
        public static int VerifyUser(string token)
        {
            var select = "SELECT token_type FROM internal.accesstoken WHERE token = @token";
            int aux = 0;
            try
            {
                using (var connection = new MySqlConnection(Conn.strConn2))
                {
                    MySqlCommand select_Aux = new MySqlCommand(select, connection);

                    select_Aux.Parameters.AddWithValue("@token", token);
                    connection.Open();

                    aux = Convert.ToInt32(select_Aux.ExecuteScalar());
                }
            }
            catch (Exception)
            {
                // Do Action
            }
            return aux;
        }

        public static DataTable get_test_table()
        {
            // New empty DataTable
            DataTable dt = new();

            // Auxiliary variable for sql statement
            var sql_select = "SELECT * FROM wb.sales_aux";

            try
            {
                // Create new connection to mysql using connection string
                using (var connection = new MySqlConnection(Conn.strConn3))
                {
                    // Create new data adapter varible to get table result of sql select statement
                    using (var data = new MySqlDataAdapter(sql_select, connection))
                    {
                        // Fill data table with content from data adapater variable
                        data.Fill(dt);
                    }
                }
            }
            catch (Exception e)
            {
                // Return empty table
                throw new Exception(e.Message);
            }
            // Return Data Table
            return dt;
        }

        public static List<Sale_Aux> Get_sale_Aux()
        {
            DataTable dt = get_test_table();
            List<Sale_Aux> s = new List<Sale_Aux>();

            string? name_aux;
            string? model_aux;
            string? factory_serial_aux;
            string? serial_control_board_aux;
            string? serial_suply_power_aux;
            string? wb_serial_aux;
            string? data_buy_aux;
            string? start_warranty_aux;
            string? warranty_type_aux;
            string? ship_local_aux;
            string? seller_aux;

            s = (from DataRow dr in dt.Rows
                 select new Sale_Aux(
            name_aux = dr["client_name"].ToString(),
            model_aux = dr["equipment"].ToString(),
            factory_serial_aux = dr["serial_factory"].ToString(),
            serial_control_board_aux = dr["serial_control_board"].ToString(),
            serial_suply_power_aux = dr["serial_power_suply"].ToString(),
            wb_serial_aux = dr["wb_serial"].ToString(),
            data_buy_aux = dr["date_buy"].ToString(),
            start_warranty_aux = dr["warranty_start_date"].ToString(),
            warranty_type_aux = dr["warranty"].ToString(),
            ship_local_aux = dr["local_ship"].ToString(),
            seller_aux = dr["seller"].ToString())).ToList();

            return s;
        }

        public static int get_id_sales_aux(string name)
        {
            var cmd = "SELECT id FROM wb.clients WHERE name = @name_aux";

            try
            {
                using (var connection = new MySqlConnection(Conn.strConn3))
                {
                    MySqlCommand cmd_aux = new MySqlCommand(cmd, connection);
                    cmd_aux.Parameters.AddWithValue("@name_aux", name);

                    connection.Open();

                    int id = Convert.ToInt32(cmd_aux.ExecuteScalar());
                    return id;

                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Adicionar Cliente a base de dados
        /// </summary>
        /// <param name="c"<></param>
        public static bool Add_Test(Sale_Aux s)
        {
            // Atribuir a uma variável o comando SQL para inserir os dados e seus respetivos valores
            var sqlInsert = "INSERT INTO wb.sales(client_id, cli_name, equipment, serial_factory, serial_control_board, serial_power_suply, wb_serial, date_buy, warranty, warranty_start_date, local_ship, seller) VALUES(@s_client_id, @s_cli_name, @s_equipment, @s_serial_factory, @s_serial_control_board, @s_serial_power_suply, @s_wb_serial, @s_data_buy, @s_warranty, @s_warranty_start_date, @s_localship, @s_seller)";

            // Iniciar processo de conexão a ao servidor com auxilio da Classe Conn que possui os dados de conexão
            try
            {
                using (var connection = new MySqlConnection(Conn.strConn3))
                {
                    // Criar novo objeto "comando" para executar comando SQL criado
                    MySqlCommand sqlInsert_Aux = new MySqlCommand(sqlInsert, connection);

                    //Adicionar parâmetros ao comando de acordo com as variáveis de entrada (método mais seguro)
                    sqlInsert_Aux.Parameters.AddWithValue("@s_client_id", s.cli_id);
                    sqlInsert_Aux.Parameters.AddWithValue("@s_cli_name", s.name);
                    sqlInsert_Aux.Parameters.AddWithValue("@s_equipment", s.model);
                    sqlInsert_Aux.Parameters.AddWithValue("@s_serial_factory", s.factory_serial);
                    sqlInsert_Aux.Parameters.AddWithValue("@s_serial_control_board", s.serial_control_board);
                    sqlInsert_Aux.Parameters.AddWithValue("@s_serial_power_suply", s.serial_suply_power);
                    sqlInsert_Aux.Parameters.AddWithValue("@s_wb_serial", s.wb_serial);
                    sqlInsert_Aux.Parameters.AddWithValue("@s_data_buy", s.data_buy);
                    sqlInsert_Aux.Parameters.AddWithValue("@s_warranty", s.warranty_type);
                    sqlInsert_Aux.Parameters.AddWithValue("@s_warranty_start_date", s.start_warranty);
                    sqlInsert_Aux.Parameters.AddWithValue("@s_localship", s.ship_local);
                    sqlInsert_Aux.Parameters.AddWithValue("@s_seller", s.seller);

                    // Abrir conexão com base de dados
                    connection.Open();

                    // Executar comando SQL
                    int aux = Convert.ToInt32(sqlInsert_Aux.ExecuteNonQuery());
                    if (aux != 0) return true;
                    else return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                // Exibir mensagem do erro específico
            }
        }

        #endregion
    }
}


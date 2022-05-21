namespace API_VNA_2._0.BusinessObjects
{
    public class Sale_Aux
    {
        public string? name { get; set; }
        public int cli_id { get; set;  }
        public string? model { get; set; }
        public string? factory_serial { get; set; }
        public string? serial_control_board { get; set; }
        public string? serial_suply_power { get; set; }
        public string? wb_serial { get; set; }
        public string? data_buy { get; set; }
        public string? start_warranty { get; set; }
        public string? warranty_type { get; set; }
        public string? ship_local { get; set; }
        public string? seller { get; set; }

        public Sale_Aux(string? name, string ? model, string? factory_serial, string ? serial_control_board, string? serial_suply_power, string? wb_serial, string? data_buy, string? start_warranty, string? warranty_type, string? ship_local, string? seller)
        {
            this.name = name;
            this.model = model;
            this.factory_serial = factory_serial;
            this.serial_control_board = serial_control_board;
            this.serial_suply_power = serial_suply_power;
            this.wb_serial = wb_serial;
            this.data_buy = data_buy;
            this.start_warranty = start_warranty;
            this.warranty_type = warranty_type;
            this.ship_local = ship_local;
            this.seller = seller;
        }

    }
}

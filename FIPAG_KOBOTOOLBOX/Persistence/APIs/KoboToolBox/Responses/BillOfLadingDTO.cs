using Newtonsoft.Json;

namespace FIPAG_KOBOTOOLBOX.Persistence.APIs.KoboToolBox.Responses
{
    public class BillOfLadingDTO
    {
        public string number { get; set; }
        public string usage { get; set; }
        public double manifested_count { get; set; }
        public int? cargo_at_terminal_destination_count { get; set; }
        public int? cargo_at_terminal_count { get; set; }
        public int? cargo_delivered_count { get; set; }
        public int? packing_list_items_at_terminal_destination_count { get; set; }
        public int? packing_list_items_at_terminal_count { get; set; }
        public int? packing_list_items_delivered_count { get; set; }
        public string remarks { get; set; }
        public double weight { get; set; }
        public double? volume_in_cubic_meters { get; set; }
        public double? volume { get; set; }
        public string goods { get; set; }
        public string destination_code { get; set; }
        public string type_of_contract { get; set; }

        public string consignee_address { get; set; }
        public string exporter_address { get; set; }
        public string shipping_marks { get; set; }
        public bool? packing_list_expected { get; set; }
        public bool? has_packing_list_items { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}

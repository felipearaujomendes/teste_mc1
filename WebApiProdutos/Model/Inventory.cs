using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiProdutos.Model
{
    public partial class Inventory
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("quantity")]
        public long Quantity { get; set; }

        [JsonProperty("warehouses")]
        public List<Warehouse> Warehouses { get; set; }


        [JsonProperty("FK_IdWarehouse")]
        public int FK_IdWarehouse { get; set; }
    }
}

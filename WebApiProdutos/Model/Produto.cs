using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiProdutos.Model
{


    public partial class Produto
    {
        [JsonProperty("sku")]
        public long Sku { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("inventory")]
        public Inventory inventory { get; set; }

        [JsonProperty("isMarketable")]
        public bool IsMarketable { get; set; }

        [JsonProperty("FK_Inventory")]
        public int FK_Inventory { get; set; }
    }
 
}


using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiProdutos.Model
{
    public partial class Warehouse
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("locality")]
        public string Locality { get; set; }

        [JsonProperty("quantity")]
        public long Quantity { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}

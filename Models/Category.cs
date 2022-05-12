using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Ecommerce_API.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [JsonIgnore]

        public virtual ICollection<Product> Products { get; set; }
    }
}

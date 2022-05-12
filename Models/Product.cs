using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Ecommerce_API.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Photo { get; set; }
        public DateTime PurchaseDate { get; set; }
        [ForeignKey("Category")]
        public int CateogryID { get; set; }
        [JsonIgnore]
        public virtual Category Category { get; set; }
    }
}

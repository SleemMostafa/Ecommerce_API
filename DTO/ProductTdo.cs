using Ecommerce_API.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce_API.DTO
{
    public class ProductTdo
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Photo { get; set; }
        public DateTime PurchaseDate { get; set; }
        [Required]
        public int CateogryID { get; set; }
    }
}

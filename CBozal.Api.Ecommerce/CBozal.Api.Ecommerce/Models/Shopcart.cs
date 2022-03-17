using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CBozal.Api.Ecommerce.Models
{
    public class Shopcart
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
        public string ProductName => Product.Name;
        public decimal Price => Product.Price;
        public decimal Total => Product.Price * Quantity;
    }
}

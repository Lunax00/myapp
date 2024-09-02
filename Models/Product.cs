﻿using System.ComponentModel.DataAnnotations;

namespace MyApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public string? Category { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime DateAdded { get; set; }
    }
}

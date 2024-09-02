using System;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Models
{
    public class StockMovement
    {
        public int Id { get; set; }
        public DateTime MovementDate { get; set; }
        public int ProductId { get; set; } // Foreign key
        public int Quantity { get; set; }
        public string MovementType { get; set; } // e.g., Inbound, Outbound
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Data.Models
{
    public class TBProducts
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; } 
        public int Qty { get; set; } = 0;
        public List<TBOrders> Orders { get; set; } = new List<TBOrders>();    
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Data.Models
{
    public class TBOrders
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public double TotalPrice { get; set; }
        public List<TBProducts> Products { get; set; } = new List<TBProducts>();

    }
}

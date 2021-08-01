using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.DTOs
{
    public class DTOOrder
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public double TotalPrice { get; set; }
    }
}

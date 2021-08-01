using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.DTOs
{
    
    public class DTOOrderDetails
    {
        public Guid id { get; set; }
        public DateTime date { get; set; }
        public double totalPrice { get; set; }
        
        public List<DTOProductForm> products { get; set; } = new List<DTOProductForm>();
    }
}

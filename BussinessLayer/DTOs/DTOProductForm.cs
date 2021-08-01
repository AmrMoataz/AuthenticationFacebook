using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BussinessLayer.DTOs
{
    public class DTOProductForm
    {

        public Guid Id { get; set; } 
        public string Name { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }

    }
}

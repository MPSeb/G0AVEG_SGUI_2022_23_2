using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace G0AVEG_ADT_2022_23_1.Models
{
    public class Wood
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public virtual List<Furniture> Furnitures { get; set; }

    }
}

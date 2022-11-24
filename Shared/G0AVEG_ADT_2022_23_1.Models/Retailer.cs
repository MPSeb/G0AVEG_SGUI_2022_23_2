using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G0AVEG_ADT_2022_23_1.Models
{
    public class Retailer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual  List<Furniture> furnitures { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G0AVEG_ADT_2022_23_1.Models
{
    public class Furniture
    {
        private string name;

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get =>name; set { name = value;  PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FurnitureName")); } }

        [ForeignKey(nameof(wood))]
        public int? WoodUsed { get; set; }
        public Wood wood { get; set; }

        [ForeignKey(nameof(retailer))]
        public int? RetailerId { get; set; }
        public Retailer retailer { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}

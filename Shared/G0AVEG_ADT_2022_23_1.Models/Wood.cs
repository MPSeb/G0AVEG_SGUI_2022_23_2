using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace G0AVEG_ADT_2022_23_1.Models
{
    public class Wood
    {
        private string name;
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get => name; set { name = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FurnitureName")); } }
        public int Price { get; set; }

        public virtual List<Furniture> Furnitures { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

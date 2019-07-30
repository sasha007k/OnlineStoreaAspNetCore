using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    public class PhoneModel
    {
        public Guid ID { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public double Price { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;


namespace NewApi.Models {
    public class Car {
        
        [Key]
        public Guid id { get; set; }

        [Required]
        public string name {get;set;}

        [Required]
        public string color {get;set;}


        public Car() {
            id = Guid.NewGuid();
        }
    }
}
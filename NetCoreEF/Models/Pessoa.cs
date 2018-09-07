using System;
using System.ComponentModel.DataAnnotations;

namespace NewApi.Models {
    public class Pessoa {

        [Key]
        public Guid id {get;set;}

        [Required(ErrorMessage="Campo Nome obrigatório")]
        public string nome{get;set;}

        [Required(ErrorMessage="Campo Telefone obrigatório")]
        [MaxLength(10, ErrorMessage = "Tamanho m[aximo de 10")]
        public string telefone {get;set;}

        public virtual Grupo grupo { get; set; }

        public Pessoa() {
            id = Guid.NewGuid();
        }

        public Pessoa(string n, string t)
        {
            id = Guid.NewGuid();
            nome = n;
            telefone = t;
        }
    }
}
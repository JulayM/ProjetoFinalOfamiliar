using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OFamiliar.Models
{
    public class Familia
    {
        //Construtor da classe
        public Familia()
        {
        //carrega listas das classes referenciadas 
        ListaDeMovimentos = new HashSet<Movimentos>();
        ListaDeConvite = new HashSet<Convite>();
        ListaDeMembros = new HashSet<Pessoas>();
        }
        [Key]//indica que o atributo é PK
       // [DatabaseGenerated(DatabaseGeneratedOption.None)] // marcar o atributo como não auto number
        [Display(Name = "Identificador da Família")]
        public int FamiliaID { get; set; }

        [Display(Name = "Nome da Família")]
        [Required(ErrorMessage = "O {0} é do preenchimento obrigatório...")]
        [RegularExpression("[A-ZÍÂÓ][a-záéíóúàèìòùâêîôûãõäëïöüç']+((-| )((de|da|do|dos) )?[A-ZÍÂÓ][a-záéíóúàèìòùâêîôûãõäëïöüç']+)*",
               ErrorMessage = "No {0} só são aceites letras. Cada nome começa, obrigatoriamente, por uma maiúscula...")]
        public string Nome { get; set; }

        //só regista 'datas', não 'horas'
        [Column(TypeName = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Data da Criação")]
        public DateTime DataDeCriacao { get; set; }
        // lista os 'movimentos' de uma família
        public virtual ICollection<Movimentos> ListaDeMovimentos { get; set; }
        //lista os 'convites' de uma família
        public virtual ICollection<Convite> ListaDeConvite { get; set; }

        // lista os 'membros' de uma família
        public virtual ICollection<Pessoas> ListaDeMembros { get; set; }

     
        
    }
}
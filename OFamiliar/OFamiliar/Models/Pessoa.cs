using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OFamiliar.Models
{
    public class Pessoas
    {
        //Construtor da Classe
        public Pessoas()
        {
            //carrega listas das classes referenciadas 
            ListaConvitesRecebidos = new HashSet<Convite>();
            ListaConvitesEmitidos = new HashSet<Convite>();
            ListaMovimentos= new HashSet<Movimentos>();
            ListaDeFamilias = new HashSet<Familia>();
        }

        [Key]//indica que o atributo é um chave primaria "PK"
     // [DatabaseGenerated(DatabaseGeneratedOption.None)] // marcar o atributo como não auto number
        [Display(Name = "Identificador da Pessoa")]
        public int PessoaID { get; set; }

        [Required(ErrorMessage = "O {0} é do preenchimento obrigatório...")]
        [Display(Name = "Nome da Pessoa")]
        [StringLength(30)]
        public string Nome { get; set; }

       
        [Display(Name = "Data de Nascimento")]
        //só regista 'datas', não 'horas'
        [Column(TypeName = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DataNascimento { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "O endereço do e-mail não é valido.")]
        public string Email { get; set; }
        [StringLength(30)]
        public string Telefone { get; set; }

        public string Genero { get; set; }
        
       // [StringLength(9)]
        public string NIF { get; set; }

        // Uma pessoa tem uma coleção de convites recebidos
        public virtual ICollection<Convite> ListaConvitesRecebidos { get; set; }
        // Uma pessoa tem uma coleção de movimentos
        public virtual ICollection<Movimentos> ListaMovimentos { get; set; }
        // Uma pessoa tem uma coleção de convites enviados
        public virtual ICollection<Convite> ListaConvitesEmitidos { get; set; }
        // Um pessoa tem uma coleção de famila
        public virtual ICollection<Familia> ListaDeFamilias { get; set; }
    }
}
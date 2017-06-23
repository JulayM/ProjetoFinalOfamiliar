using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OFamiliar.Models
{
    public class Categoria
    {
        //Cria o construtor da classe e carrega a lista dos Movimentos
        public Categoria()
        {
            //inicialização da lista dos Movimentos
            ListaDeMovimentos = new HashSet<Movimentos>();

        }
        //indica que o atributo é PK
        [Key]
        [Display(Name = "Identificador de Categoria")]
        public int CategoriaID { get; set; }

        [StringLength(30)]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Tipo de Movimento")]
        public string Tipo { get; set; }

        // Especifica que a 'Categoria' tem muitos Movimentos
        public virtual ICollection<Movimentos> ListaDeMovimentos { get; set; }

       
        }
    
}
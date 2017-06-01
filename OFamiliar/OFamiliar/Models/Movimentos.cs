using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OFamiliar.Models
{
    public class Movimentos
    {
        //indica que o atributo é um chave primaria "PK"
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)] // marcar o atributo como não auto number
        [Display(Name = "Identificador do Movimento")]
        public int MovimentosID { get; set; }

        [Display(Name = "Data do Movimento")]
        //só regista 'datas', não 'horas'
        [Column(TypeName = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Data { get; set; }//Inicializa uma nova instância da estrutura DateTime para o ano, o mês e o dia especificados.

        [Required]
        [Display(Name = "Montante")]
        public double Valor { get; set; }

        [Required]
        public string Moeda { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        //***************************************************************************
        [ForeignKey("DonoDoMovimento")]
        public int DonoDoMovimentoFK { get; set; }//existe para criar a FK NA BASE DE Ddos
        public Pessoas DonoDoMovimento { get; set; }//existe para relacionar os objectos
        //***************************************************************************
        [ForeignKey("Familia")]
        public int FamiliasFK { get; set; }//existe para criar a FK NA BASE DE Ddos
        public Familia Familia { get; set; }//existe para relacionar os objectos
        //**************************************************************************
        [ForeignKey("Categoria")]
        public int CategoriaFK { get; set; }//existe para criar a FK NA BASE DE Ddos
        public Categoria Categoria { get; set; }//existe para relacionar os objectos


    }
}
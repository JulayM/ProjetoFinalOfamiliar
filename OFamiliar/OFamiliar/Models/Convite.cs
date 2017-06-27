using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OFamiliar.Models
{
    public class Convite
    {
        [Key]//Indica que o atributo é PK
        //[DatabaseGenerated(DatabaseGeneratedOption.None)] // marcar o atributo como não auto number
        [Display(Name = "Identificador do Convite")]
        public int ConviteID { get; set; }

        //só regista 'datas', não 'horas'
        [Column(TypeName = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
       // [RegularExpression("([0-9]{4}-[0-9]{2}-[0-9]{2})*", ErrorMessage = "No {0} só é aceite o formato yyyy-MM-dd")]
        public DateTime Data { get; set; }

        public string Token { get; set; } // código que identifica o convite de forma única
        [Display(Name = "Estado do Convite")]
        public string  EstadoDoConvite { get; set; } // representa o estado do convite: pendente, aceite, recusado

        //criação das  chaves forasteira -FK
        [ForeignKey("EmissorFK")]
        public Pessoas Emissor { get; set; }//Uma Pessoa faz um ou mais  convite
        public int EmissorFK { get; set; }

        //*****************************************************
        [ForeignKey("DestinatarioFK")] //Uma Pessoa recebe um ou mais convite
        public Pessoas Destinatario { get; set; }
        public int DestinatarioFK { get; set; }

        //******************************************************************
        [ForeignKey("FamiliarFK")]       
        public Familia Familiar { get; set; }
        public int FamiliarFK { get; set; }

        //******************************************************************
       
        
    }
}
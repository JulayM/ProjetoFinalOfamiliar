using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OFamiliar.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }
    }

    public class ForgotViewModel
    {

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name ="Endereço de e-mail")]
        [StringLength(30)]
                public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name ="Chave de acesso")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="Repetir chave de acesso")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        //***********************************************************
        // dados específicos de uma PESSOA

        [Required(ErrorMessage = "o {0} é de preenchimento obrigatório")]
        //  [Display(Name = "Nome do Dono d")]
        [RegularExpression("[A-ZÍÂÓ][a-záéíóúàèìòùâêîôûãõäëïöüç']+((-| )((de|da|do|dos) )?[A-ZÍÂÓ][a-záéíóúàèìòùâêîôûãõäëïöüç']+)*",
           ErrorMessage = "No {0} só são aceites letras. Cada nome começa, obrigatoriamente, por uma maiúscula...")]
        public string Nome { get; set; }


        [Display(Name = "Data de Nascimento")]
        //só regista 'datas', não 'horas'
        [Column(TypeName = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DataNascimento { get; set; }
               
        [StringLength(9)]
        [RegularExpression("[0-9]{9}", ErrorMessage = "O {0} não é valido.")]
        public string Telefone { get; set; }

        [StringLength(1)]
        public string Genero { get; set; }

        [Required]
        [StringLength(9)]
        [RegularExpression("[0-9]{9}", ErrorMessage = "Escreva apenas 9 carateres numéricos...")]
        public string NIF { get; set; }





    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
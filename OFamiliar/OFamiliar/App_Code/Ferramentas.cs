using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;

namespace OFamiliar.App_Code
{
    public class Ferramentas
    {

        /// <summary>
        /// Função para enviar um email
        /// </summary>
        /// <param name="destinatarioEmail">Email do destinatário</param>
        /// <param name="subject">Título de Email</param>
        /// <param name="body">Corpo do Email</param>
        /// <returns></returns>
        public static int sendEmail(string destinatarioEmail, string subject, string body)
        {

            int resultado = 0; // var para exprimir o sucesso da operação de enviar um email: 0 - significa SUCESSO pleno

            // recupera o endereço de email a utilizar no envio da mensagem
            string emailFrom = WebConfigurationManager.AppSettings["email"].ToString();

            using (var client = new SmtpClient())
            {
                // envia um email
                // quando se utiliza o servidor de email do GOOGLE é necessário baixar a segurança da conta
                // senão o Google bloqueia o envio de emails
                // https://myaccount.google.com/lesssecureapps?pli=1

                try
                {
                    using (var message = new MailMessage(emailFrom, destinatarioEmail))
                    {
                        message.Subject = subject;
                        message.Body = body;

                        message.IsBodyHtml = true;

                        client.EnableSsl = true;
                        client.Send(message);
                    } // fim do 'using' (MailMessage)
                }
                catch (Exception)
                {
                    // ocorreu um erro

                    resultado = 1;
                } // fim TRY/CATCH

            } // fim do 'using' (SmtpClient)


            return resultado; // significa q enviou com sucesso
        } // Fim do send email


    }
}
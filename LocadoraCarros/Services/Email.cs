using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LocadoraCarros.Services
{
    public class Email : IEmail
    {
        private ConfiguracoesEmail _configuracoesEmail;
        public Email(IOptions<ConfiguracoesEmail> configuracoesEmail)
        {
            _configuracoesEmail = configuracoesEmail.Value;
        }

        public async Task EnviarEmail(string email, string assunto, string mensagem)
        {
            var destinatario = string.IsNullOrEmpty(email) ? _configuracoesEmail.Email : email;

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(_configuracoesEmail.Email, "Willams")
            };

            mailMessage.To.Add(destinatario);
            mailMessage.Subject = assunto;
            mailMessage.Body = mensagem;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.High;

            using(SmtpClient smtpClient = new SmtpClient(_configuracoesEmail.Endereco, _configuracoesEmail.Porta))
            {
                smtpClient.Credentials = new NetworkCredential(_configuracoesEmail.Email, _configuracoesEmail.Senha);
                smtpClient.EnableSsl = true;
                await smtpClient.SendMailAsync(mailMessage);
            }

        }
    }
}

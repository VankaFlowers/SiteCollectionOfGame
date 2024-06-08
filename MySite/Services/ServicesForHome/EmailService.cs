using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
namespace MySite.Services.ServicesForHome
{
    public class SmtpSettings
    {
        public string? Server { get; set; } = "smtp.yandex.com";
        public int Port { get; set; } = 465; //465
        public string? SenderName { get; set; } = "AmnyamLibGames";
        public string? SenderEmail { get; set; } = "amnyamgame@yandex.ru";
        public string? Username { get; set; } = "amnyamgame";
        public string? Password { get; set; } = "wypweylugoxxkges";
    }


    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html) 
            { 
                Text = body
            };

            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, SecureSocketOptions.SslOnConnect);//SecureSocketOptions.SslOnConnect
                await smtp.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
                await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);
            }
        }
    }
}

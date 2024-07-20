using System.Net;
using System.Net.Mail;
using BlogSite.Data;
using BlogSite.Dto;

namespace BlogSite.Service
{
    public class EmailService
    {
        private readonly ApplicationDbContext _context;

        public EmailService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task SendEmail(SendEmailDto input)
        {
            var fromEmail = "shubhi.191999@gmail.com";
            var fromEmailPassword = "cioq ricb yxiw zbxa";
            var message = new MailMessage()
            {
                From = new MailAddress(fromEmail),
                Subject = input.subject,
                Body = input.body,
                IsBodyHtml = true
            };
            message.To.Add(input.toEmail);
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromEmail, fromEmailPassword),
                EnableSsl = true,
            };
            smtpClient.Send(message);
        }
    }
}
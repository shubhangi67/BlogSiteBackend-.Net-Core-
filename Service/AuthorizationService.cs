using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using BlogSite.Data;
using BlogSite.Dto;
using BlogSite.ViewModel.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BlogSite.Service
{
    public class AuthorizationService
    {
        private string _mailToken = "MailSent";

        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<AuthorizationService> _logger;
        public AuthorizationService(ApplicationDbContext dbContext, ILogger<AuthorizationService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<AccountDto> GenerateJwtToken(LoginRequestViewModel loginRequestViewModel)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var user = await _dbContext.User.FirstOrDefaultAsync(x => x.Email == loginRequestViewModel.Email && x.Password == loginRequestViewModel.Password);
            if (user == null)
            {
                _logger.LogError("User not found");
                return null;
            }
            try
            {
                var claims1 = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user?.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())

            };
            }
            catch (Exception ex)
            {

            }
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user?.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())

            };
            var expires = DateTime.Now.AddDays(Convert.ToDouble(JwtSettings.ExpirationInHours));
            var token = new JwtSecurityToken(
                issuer: JwtSettings.Issuer,
                audience: JwtSettings.Audience,
                claims,
                expires: expires,
                signingCredentials: credentials
            );
            return new AccountDto()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Id = user.Id,
            };
            return new AccountDto();
        }
        public async Task SendEmail(string toEmail, string subject, string body)
        {
            var fromEmail = "shubhi.191999@gmail.com";
            var fromEmailPassword = "cioq ricb yxiw zbxa";
            var message = new MailMessage()
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            message.To.Add(toEmail);
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromEmail, fromEmailPassword),
                EnableSsl = true,
            };
            smtpClient.Send(message);
        }
        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            else if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
        }
    }
    public static class JwtSettings
    {
        public const string Issuer = "https://localhost:44355/";
        public const string Audience = "https://localhost:5041/";
        public const string Secret = "your_secret_key_with_at_least_32_bytes_here";
        public const int ExpirationInHours = 6;
    }

}
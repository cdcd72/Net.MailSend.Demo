using MailSend.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MailSend.SendGrid
{
    /// <summary>
    /// SendGrid https://sendgrid.com/
    /// </summary>
    public class SendGrid
    {
        private readonly SendGridClient _sendGridClient;

        public SendGrid(IConfiguration config, IServiceProvider serviceProvider)
        {
            var apiKey = config.GetValue<string>($"{nameof(SendGrid)}:ApiKey");
            _sendGridClient = 
                new SendGridClient(
                    serviceProvider.GetService<IHttpClientFactory>().CreateClient(), 
                        new SendGridClientOptions { ApiKey = apiKey, HttpErrorAsException = true });
        }

        public async Task<string> SendEmailAsync(SendGridMail mail)
        {
            var response = 
                await _sendGridClient.RequestAsync(BaseClient.Method.POST, mail.ToJson(), null, "mail/send");

            return await response.Body.ReadAsStringAsync();
        }
    }
}

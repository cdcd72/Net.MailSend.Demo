using MailSend.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MailSend.Implement
{
    /// <summary>
    /// 一般方式送信
    /// </summary>
    public class NormalSendMail : BaseSendMail, ISendMail
    {
        private readonly IConfiguration _config;

        public NormalSendMail(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// 送信
        /// </summary>
        /// <returns></returns>
        public async Task<string> SendMailAsync()
        {
            // 下方雖然還是透過 SendGrid 來送信，但可以自行再調整，範例供參考...

            HttpClient client = new HttpClient() { BaseAddress = new Uri("https://api.sendgrid.com/v3/") };

            client.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", _config.GetValue<string>($"{nameof(SendGrid.SendGrid)}:ApiKey"));

            HttpResponseMessage response =
                await client.PostAsync("mail/send",
                    new StringContent(GetSendGridMail().ToJson(), Encoding.UTF8, "application/json"));

            return await response.Content.ReadAsStringAsync();
        }
    }
}

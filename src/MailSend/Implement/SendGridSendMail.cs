using MailSend.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace MailSend.Implement
{
    /// <summary>
    /// SendGrid 送信
    /// </summary>
    public class SendGridSendMail : BaseSendMail, ISendMail
    {
        private readonly IConfiguration _config;
        private readonly IServiceProvider _serviceProvider;

        public SendGridSendMail(IConfiguration config, IServiceProvider serviceProvider)
        {
            _config = config;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 送信
        /// </summary>
        /// <returns></returns>
        public async Task<string> SendMailAsync()
        {
            var sendGrid = new SendGrid.SendGrid(_config, _serviceProvider);
            return await sendGrid.SendEmailAsync(GetSendGridMail());
        }
    }
}

using MailSend.Enum;
using MailSend.Interface;
using MailSend.Implement;
using Microsoft.Extensions.Configuration;
using System;

namespace MailSend
{
    /// <summary>
    /// 送信實例工廠
    /// </summary>
    public class SendMailFactory
    {
        private readonly IConfiguration _config;
        private readonly IServiceProvider _serviceProvider;

        public SendMailFactory(IConfiguration config, IServiceProvider serviceProvider)
        {
            _config = config;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 創建
        /// </summary>
        /// <param name="sendBy">透過誰來幫忙送信</param>
        /// <returns></returns>
        public ISendMail Create(SendBy sendBy)
        {
            ISendMail sendMailAsync = sendBy switch
            {
                SendBy.Normal => new NormalSendMail(_config),
                SendBy.SendGrid => new SendGridSendMail(_config, _serviceProvider),
                _ => null,
            };

            return sendMailAsync;
        }
    }
}

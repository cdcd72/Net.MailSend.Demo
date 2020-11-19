using MailSend.Model;
using System.Collections.Generic;

namespace MailSend.Implement
{
    /// <summary>
    /// SendMail 基底
    /// </summary>
    public class BaseSendMail
    {
        /// <summary>
        /// 取得 SendGrid Mail 格式資料
        /// </summary>
        /// <returns></returns>
        protected SendGridMail GetSendGridMail()
        {
            List<SendGridMail.Personalization> personalizations = new List<SendGridMail.Personalization>()
            {
                new SendGridMail.Personalization()
                {
                    to = new List<SendGridMail.To>()
                    {
                        new SendGridMail.To()
                        {
                            email = "cdcd71517@gmail.com",
                            name = "Neil Tsai"
                        }
                    }.ToArray(),
                    subject = "這封給你們這群人啦"
                },
                new SendGridMail.Personalization()
                {
                    to = new List<SendGridMail.To>()
                    {
                        new SendGridMail.To()
                        {
                            email = "0351008@nkust.edu.tw",
                            name = "Neil Tsai"
                        }
                    }.ToArray(),
                    subject = "這封不看就算了啦"
                }
            };

            List<SendGridMail.Content> contents = new List<SendGridMail.Content>()
            {
                new SendGridMail.Content()
                {
                    type = "text/plain",
                    value = "測試信件啦"
                }
            };

            SendGridMail mail = new SendGridMail()
            {
                personalizations = personalizations.ToArray(),
                from = new SendGridMail.From()
                {
                    email = "neil_tsai@gss.com.tw",
                    name = "Neil Tsai"
                },
                subject = "測試使用 SendGrid 送信",
                content = contents.ToArray()
            };

            return mail;
        }
    }
}

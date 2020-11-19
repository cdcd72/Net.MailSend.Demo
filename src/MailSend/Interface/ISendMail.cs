using System.Threading.Tasks;

namespace MailSend.Interface
{
    /// <summary>
    /// 送信介面
    /// </summary>
    public interface ISendMail
    {
        /// <summary>
        /// 送信
        /// </summary>
        /// <returns></returns>
        Task<string> SendMailAsync();
    }
}

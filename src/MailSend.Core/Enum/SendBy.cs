namespace MailSend.Core.Enum
{
    public enum SendBy
    {
        /// <summary>
        /// 一般情況來送信
        /// </summary>
        Normal,
        /// <summary>
        /// 透過 SendGrid 來送信
        /// </summary>
        SendGrid
    }
}

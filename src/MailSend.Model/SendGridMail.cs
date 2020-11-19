using System.Text.Json;

namespace MailSend.Model
{
    /// <summary>
    /// SendGrid Mail
    /// </summary>
    public class SendGridMail
    {
        #region 格式定義(https://sendgrid.com/docs/API_Reference/api_v3.html)

        // 這邊並沒有把全部的格式都定義進來，視需求可自行異動...

        public Personalization[] personalizations { get; set; }

        public From from { get; set; }

        public string subject { get; set; }

        public Content[] content { get; set; }

        #endregion

        #region 類別定義

        /// <summary>
        /// 客製化
        /// </summary>
        public class Personalization
        {
            /// <summary>
            /// 給誰
            /// </summary>
            public To[] to { get; set; }

            /// <summary>
            /// 信件主旨(可覆寫全域主旨)
            /// </summary>
            public string subject { get; set; }
        }

        /// <summary>
        /// 哪來
        /// </summary>
        public class From
        {
            /// <summary>
            /// 信箱
            /// </summary>
            public string email { get; set; }

            /// <summary>
            /// 名字
            /// </summary>
            public string name { get; set; }
        }

        /// <summary>
        /// 給誰
        /// </summary>
        public class To
        {
            /// <summary>
            /// 信箱
            /// </summary>
            public string email { get; set; }

            /// <summary>
            /// 名字
            /// </summary>
            public string name { get; set; }
        }

        /// <summary>
        /// 信件內容
        /// </summary>
        public class Content
        {
            /// <summary>
            /// MIME Type
            /// </summary>
            public string type { get; set; }

            /// <summary>
            /// 實際內容
            /// </summary>
            public string value { get; set; }
        }

        #endregion

        #region 行為定義

        /// <summary>
        /// 轉為 JSON 字串
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        #endregion
    }
}

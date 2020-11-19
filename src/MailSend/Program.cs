using MailSend.Enum;
using MailSend.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MailSend
{
    static class Program
    {
        /// <summary>
        /// 組態設定
        /// </summary>
        private static IConfiguration Config => InitializeConfiguration();

        /// <summary>
        /// 服務提供者
        /// </summary>
        private static IServiceProvider ServiceProvider => InitializeServiceProvider();

        /// <summary>
        /// 主程式
        /// </summary>
        /// <param name="args">參數</param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {
            // 透過誰來幫忙送信
            SendBy sendBy;

#if DEBUG
            if (args.Length == 0)
                args = new string[] { "Normal" }; // 表示使用通常方式來送信，測試可自行再調整...
#endif

            if (args.Length > 0)
            {
                sendBy = (SendBy)System.Enum.Parse(typeof(SendBy), args[0]);

                // 決定送信策略
                ISendMail sendMail = new SendMailFactory(Config, ServiceProvider).Create(sendBy);

                // 送信
                string responseString = await sendMail.SendMailAsync();

                // 確認
                Console.WriteLine(string.IsNullOrEmpty(responseString) ? "Success" : $"Fail: {responseString}");
                Console.ReadLine();
            }
        }

        #region Initialize

        /// <summary>
        /// 初始化組態設定
        /// </summary>
        /// <returns></returns>
        static IConfiguration InitializeConfiguration()
        {
            JsonConfigurationSource releaseJsonSource = new JsonConfigurationSource()
            {
                FileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory()),
                Path = "mailsettings.json",
                Optional = true,
                ReloadOnChange = true
            };

            JsonConfigurationSource otherJsonSource = new JsonConfigurationSource()
            {
                FileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory()),
                Path = $"mailsettings.{Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT")}.json",
                Optional = true,
                ReloadOnChange = true
            };

            return new ConfigurationBuilder()
                    .Add(releaseJsonSource)
                    .Add(otherJsonSource)
                    .Build();
        }

        /// <summary>
        /// 初始化注入服務
        /// </summary>
        /// <returns></returns>
        static IServiceProvider InitializeServiceProvider()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddHttpClient();

            return serviceCollection.BuildServiceProvider();
        }

        #endregion
    }
}

using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace General.Extension
{
    public class EmailExtension
    {
        private readonly EmailSettings emailSettings;
        private static EmailExtension instance;
        private static object mlock = new object();

        public static EmailExtension Instance
        {
            get
            {
                lock (mlock)
                {
                    if (instance == null)
                        instance = new EmailExtension();

                    return instance;
                }
            }
        }
        public EmailExtension()
        {
            emailSettings = new EmailSettings();

            emailSettings.SMTPSenderName = SqlDAL.Instance.GetSetting("SMTPSenderName").Value;
            emailSettings.SMTPSender = SqlDAL.Instance.GetSetting("SMTPSender").Value;
            emailSettings.SMTPPassword = SqlDAL.Instance.GetSetting("SMTPPassword").Value;
            emailSettings.SMTPMailServer = SqlDAL.Instance.GetSetting("MailServer").Value;
        }
        public async Task<bool> SendEmailAsync(string email, string subject, string message)
        {
            bool rsBool = false;
            try
            {
                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(emailSettings.SMTPSenderName, emailSettings.SMTPSender));

                mimeMessage.To.Add(new MailboxAddress(email));

                mimeMessage.Subject = subject;

                mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = message
                };
                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    await client.ConnectAsync(emailSettings.SMTPMailServer, emailSettings.SMTPMailPort, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(emailSettings.SMTPSender, emailSettings.SMTPPassword);

                    await client.SendAsync(mimeMessage);

                    await client.DisconnectAsync(true);
                }
                rsBool = true;
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return rsBool;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Helper
{
   public class EmailHelper
    {
        public static bool SendEmail(List<string> To, List<string> CC, List<string> BCC, string Subject = null, string Body = null, string OrgName = null,  List<string> attachments = null, AlternateView altview = null, Attachment attachment = null)
        {
            MailMessage Message = new MailMessage();
            SmtpClient Smtp = new SmtpClient(ConfigurationManager.AppSettings["MailHost"].ToString(), Convert.ToInt16(ConfigurationManager.AppSettings["MailPort"].ToString()));
            NetworkCredential NetworkInfo = new NetworkCredential(ConfigurationManager.AppSettings["MailUserName"].ToString(), ConfigurationManager.AppSettings["MailPassword"].ToString());
            MailAddress ReceiptAddress;
            char[] delimiters = new[] { ',', ';' };
            try
            {
                if (To.Count > 0)
                {
                    foreach (var item in To)
                    {
                        foreach (var itemTo in item.Split(delimiters, StringSplitOptions.RemoveEmptyEntries))
                        {
                            if (!String.IsNullOrWhiteSpace(itemTo))
                            {
                                ReceiptAddress = new MailAddress(itemTo);
                                Message.To.Add(ReceiptAddress);
                            }
                        }
                    }
                }
                if (BCC.Count > 0)
                {
                    foreach (var item in BCC)
                    {
                        foreach (var itemBcc in item.Split(delimiters, StringSplitOptions.RemoveEmptyEntries))
                        {
                            if (!String.IsNullOrWhiteSpace(itemBcc))
                            {
                                ReceiptAddress = new MailAddress(itemBcc);
                                Message.Bcc.Add(ReceiptAddress);
                            }
                        }
                    }
                }
                if (CC.Count > 0)
                {
                    foreach (var item in CC)
                    {
                        foreach (var itemCC in item.Split(delimiters, StringSplitOptions.RemoveEmptyEntries))
                        {
                            if (!String.IsNullOrWhiteSpace(itemCC))
                            {
                                ReceiptAddress = new MailAddress(itemCC);
                                Message.CC.Add(ReceiptAddress);
                            }
                        }
                    }
                }
               
                if (attachments != null && attachments.Count > 0)
                {
                    foreach (var attch in attachments)
                    {
                        string filepath = attch;
                        string filename = Path.GetFileName(filepath);
                        byte[] bytes = System.IO.File.ReadAllBytes(filepath);
                        MemoryStream stream = new MemoryStream(bytes);
                        Message.Attachments.Add(new Attachment(stream, filename));
                    }
                }
                if (altview != null)
                {
                    Message.AlternateViews.Add(altview);
                    AlternateView HTMLV = AlternateView.CreateAlternateViewFromString(Body, new System.Net.Mime.ContentType("text/html"));
                    Message.AlternateViews.Add(HTMLV);
                }
                if (attachment != null)
                {
                    Message.Attachments.Add(attachment);
                }

                Message.From = new MailAddress(ConfigurationManager.AppSettings["MailFrom"].ToString(), OrgName);

                Message.Subject = Subject;
                Message.IsBodyHtml = true;
                Message.Body = Body;


                Smtp.UseDefaultCredentials = true;
                Smtp.Credentials = NetworkInfo;
                Smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSSL"]);
                Smtp.Send(Message);

                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }
    }
}

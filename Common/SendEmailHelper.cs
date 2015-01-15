using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using DataModel;

namespace Common
{
    public class SendEmailHelper
    {

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="toname">收件人名称</param>
        /// <param name="toemail">收件人邮件地址</param>
        /// <param name="smtpclient">伺服器</param>
        /// <param name="fromname">发件人名称</param>
        /// <param name="fromemail">发件人邮件地址</param>
        /// <param name="password">发件人邮箱密码</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件主体</param>
        /// <returns></returns>
        public static bool SendEmailForGetpass(string toname, string toemail, string smtpclient, string fromname, string fromemail, string password, string subject, string body)
        {
            try
            {
                MailAddress from = new MailAddress(fromemail, fromname);
                MailAddress to = new MailAddress(toemail, toname);
                MailMessage message = new MailMessage(from, to);
                message.Subject = subject;
                message.Body = body;
                message.Priority = MailPriority.High;
                message.IsBodyHtml = true;

                SmtpClient client = new SmtpClient(smtpclient);//伺服器,如"smtp.163.com"
                client.Port = 465;
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential(fromemail, password);
                client.Send(message);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 发送邮件程序调用方法 SendMail("abc@126.com", "某某人", "cba@126.com", "你好", "我测试下邮件", "邮箱登录名", "邮箱密码", "smtp.126.com", true,);
        /// </summary>
        /// <param name="from">发送人邮件地址</param>
        /// <param name="fromname">发送人显示名称</param>
        /// <param name="to">发送给谁（邮件地址）</param>
        /// <param name="subject">标题</param>
        /// <param name="body">内容</param>
        /// <param name="username">邮件登录名</param>
        /// <param name="password">邮件密码</param>
        /// <param name="server">邮件服务器 smtp服务器地址</param>
        /// <param   name= "IsHtml "> 是否是HTML格式的邮件 </param>
        /// <returns>send ok</returns>
        public static bool SendMail(string from, string fromname, string to, string subject, string body, string server, string username, string password, bool IsHtml)
        {
            //邮件发送类
            MailMessage mail = new MailMessage();
            try
            {
                //是谁发送的邮件
                mail.From = new MailAddress(from, fromname);
                //发送给谁
                mail.To.Add(to);

                //标题
                mail.Subject = subject;
                //内容编码
                mail.BodyEncoding = Encoding.Default;
                //发送优先级
                mail.Priority = MailPriority.High;

                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                //邮件内容
                mail.Body = body;
                //是否HTML形式发送
                mail.IsBodyHtml = IsHtml;
                //邮件服务器和端口
                SmtpClient smtp = new SmtpClient();
                smtp.Host = server;
                smtp.Port = 25;

                // smtp.DeliveryFormat = SmtpDeliveryFormat.International;
                //指定发送方式
                // smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //发件人身份验证,否则163   发不了
                //smtp.UseDefaultCredentials = true;
                //指定登录名和密码
                smtp.Credentials = new System.Net.NetworkCredential(username, password);
                //超时时间
                //smtp.Timeout = 10000;
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                mail.Dispose();
            }
        }

        //读取指定URL地址的HTML，用来以后发送网页用
        public static string ScreenScrapeHtml(string url)
        {
            //读取stream并且对于中文页面防止乱码
            StreamReader reader = new StreamReader(System.Net.WebRequest.Create(url).GetResponse().GetResponseStream(), System.Text.Encoding.UTF8);
            string str = reader.ReadToEnd();
            reader.Close();
            return str;
        }

        //发送plaintxt
        public static bool SendText(string from, string fromname, string to, string subject, string body, string server, string username, string password)
        {
            return SendMail(from, fromname, to, subject, body, server, username, password, false);
        }

        //发送HTML内容
        public static bool SendHtml(string from, string fromname, string to, string subject, string body, string server, string username, string password)
        {
            return SendMail(from, fromname, to, subject, body, server, username, password, true);
        }

        //发送制定网页
        public static bool SendWebUrl(string from, string fromname, string to, string subject, string server, string username, string password, string url)
        {
            //发送制定网页
            return SendHtml(from, fromname, to, subject, ScreenScrapeHtml(url), server, username, password);

        }

        /// <summary>
        /// 默认发送格式
        /// </summary>
        /// <param name="toEmail">接受邮箱地址</param>
        /// <param name="memberid">账号id</param>
        /// <param name="fromEmail">发送邮箱地址</param>
        /// <param name="emailName">发送邮箱名字</param>
        /// <param name="smtp">smtp服务</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        public static bool SendEmailDefault(string toEmail, string memberid, string fromEmail, string emailName, string smtp, string passWord,string webUrl)
        {
            StringBuilder MailContent = new StringBuilder();
            MailContent.Append("亲爱的管理员：<br/>");
            MailContent.Append("    您好！你于");
            MailContent.Append(DateTime.Now.ToLongTimeString());
            MailContent.Append("通过<a href='#'>统一用户管理系统</a>管理中心审请找回密码。<br/>");
            MailContent.Append("    为了安全起见，请用户点击以下链接重设个人密码：<br/><br/>");
            string url = "http://" + webUrl + "/ResetPassword?id=" + memberid + "&email=" + toEmail;
            MailContent.Append("<a href='" + url + "'>" + url + "</a><br/><br/>");
            MailContent.Append("    (如果无法点击该URL链接地址，请将它复制并粘帖到浏览器的地址输入框，然后单击回车即可。)");
            //return SendEmailForGetpass("aa", ToEmail, "smtp.live.com", "123", "manageservice@live.com", "891129zz", "会员管理中心", MailContent.ToString());
            return SendHtml(fromEmail, emailName, toEmail, "找回密码", MailContent.ToString(), smtp, fromEmail, passWord);
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MvcTest.Models
{
    //邮件发送记录
    public class EmailSendHistoryR
    {

        /// <summary>
        /// 站内信ID
        /// </summary>	
        [Required]
        [Display(Name = "站内信ID")]
        public int Id { get; set; }


        /// <summary>
        /// 发送者编号ID，0=系统管理员

        /// </summary>	
        [Required]
        [Display(Name = "发送者编号ID，0=系统管理员 ")]
        public int SendUsers_Id { get; set; }


        /// <summary>
        /// 发送者账号
        /// </summary>	
        [StringLength(50, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "发送者账号")]
        public string SendUsers_Name { get; set; }


        /// <summary>
        /// 发送者邮箱
        /// </summary>	
        [StringLength(50, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "发送者邮箱")]
        public string SendUsers_Email { get; set; }


        /// <summary>
        /// 接受者编号ID，0=所有人

        /// </summary>	
        [Required]
        [Display(Name = "接受者编号ID，0=所有人 ")]
        public int RecUsers_Id { get; set; }


        /// <summary>
        /// 接收者账号
        /// </summary>	
        [StringLength(50, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "接收者账号")]
        public string RecUsers_Name { get; set; }


        /// <summary>
        /// 接收者邮箱
        /// </summary>	
        [StringLength(50, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "接收者邮箱")]
        public string RecUsers_Email { get; set; }


        /// <summary>
        /// 接受者编号ID，0=所有人

        /// </summary>	
        [Required]
        [Display(Name = "接受者编号ID，0=所有人 ")]
        public int RecUserLevel_Id { get; set; }


        /// <summary>
        /// 接受者账号
        /// </summary>	
        [StringLength(20, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "接受者账号")]
        public string RecUserLevel_Name { get; set; }


        /// <summary>
        /// 邮件主题
        /// </summary>	
        [StringLength(50, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "邮件主题")]
        public string EmailSubject { get; set; }


        /// <summary>
        /// 邮件内容
        /// </summary>	
        [Display(Name = "邮件内容")]
        public string EmailContent { get; set; }


        /// <summary>
        /// 站内信发送时间
        /// </summary>	
        [Display(Name = "站内信发送时间")]
        public DateTime SendDate { get; set; }


        /// <summary>
        /// 发送状态：0：发送失败；1发送成功

        /// </summary>	
        [Display(Name = "发送状态：0：发送失败；1发送成功")]
        public int Status { get; set; }


        /// <summary>
        /// 发送状态名称
        /// </summary>	
        [StringLength(30, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "发送状态名称")]
        public string StatusName { get; set; }


        /// <summary>
        /// 异常信息
        /// </summary>	
        [StringLength(200, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "异常信息")]
        public string ErrorMsg { get; set; }


        /// <summary>
        /// 模板ID
        /// </summary>	
        [Display(Name = "模板ID")]
        public int Template_Id { get; set; }


        /// <summary>
        /// 模板名称
        /// </summary>	
        [StringLength(30, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "模板名称")]
        public string Template_Name { get; set; }


        /// <summary>
        /// IsDel
        /// </summary>	
        [Display(Name = "IsDel")]
        public int IsDel { get; set; }   
    }
}


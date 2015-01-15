using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcTest.Models
{
    ///系统基本参数
    public class WebConfigR
    {
        /// <summary>
        /// 系统基本参数Id
        /// </summary>	
        [Required]
        [Display(Name = "系统基本参数Id")]
        public int WCID { get; set; }


        /// <summary>
        /// 基本信息--网站名称
        /// </summary>	
        [StringLength(50, ErrorMessage = "  超出了字符的最大长度 50 个字符。")]
        [Display(Name = "网站名称")]
        public string WebName { get; set; }


        /// <summary>
        /// 基本信息--网站地址
        /// </summary>	
        [StringLength(50, ErrorMessage = "  超出了字符的最大长度 50 个字符。")]
        [Display(Name = "网站地址")]
        public string WebDomain { get; set; }


        /// <summary>
        /// 基本信息--管理员邮箱
        /// </summary>	
        [StringLength(50, ErrorMessage = "  超出了字符的最大长度 50 个字符。")]
        [Display(Name = "管理员邮箱")]
        public string WebEmail { get; set; }


        /// <summary>
        /// 日志--系统登陆日志保留时间
        /// </summary>	
        [Required]
        [Display(Name = "系统登陆日志保留时间")]
        public int LoginLogReserveTime { get; set; }


        /// <summary>
        /// 日志--系统操作日志保留时间
        /// </summary>	
        [Required]
        [Display(Name = "系统操作日志保留时间")]
        public int UseLogReserveTime { get; set; }


        /// <summary>
        /// 邮件设置--SMTP服务器
        /// </summary>	
        [StringLength(50, ErrorMessage = "  超出了字符的最大长度 50 个字符。")]
        [Display(Name = "SMTP服务器")]
        public string EmailSmtp { get; set; }


        /// <summary>
        /// 邮件设置--登录用户名
        /// </summary>	
        [StringLength(50, ErrorMessage = "  超出了字符的最大长度 50 个字符。")]
        [Display(Name = "登录用户名")]
        public string EmailUserName { get; set; }


        /// <summary>
        /// 邮件设置--登录密码
        /// </summary>	
        [StringLength(50, ErrorMessage = "  超出了字符的最大长度 50 个字符。")]
        [Display(Name = "登录密码")]
        public string EmailPassWord { get; set; }


        /// <summary>
        /// 邮件设置--邮件域名
        /// </summary>	
        [StringLength(50, ErrorMessage = "  超出了字符的最大长度 50 个字符。")]
        [Display(Name = "邮件域名")]
        public string EmailDomain { get; set; }


        /// <summary>
        /// IsDel
        /// </summary>	
        [Display(Name = "IsDel")]
        public int IsDel { get; set; }
    }
}
using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MvcTest.Models
{
    //服务器端错误日志
    public class ErrorLogR
    {

        /// <summary>
        /// 主键Id
        /// </summary>	
        [Required]
        [Display(Name = "主键Id")]
        public int Id { get; set; }


        /// <summary>
        /// 出错时间
        /// </summary>	
        [Display(Name = "出错时间")]
        public DateTime ErrTime { get; set; }


        /// <summary>
        /// 浏览器版本
        /// </summary>	
        [StringLength(20, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "浏览器版本")]
        public string BrowserVersion { get; set; }


        /// <summary>
        /// 浏览器
        /// </summary>	
        [StringLength(20, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "浏览器")]
        public string BrowserType { get; set; }


        /// <summary>
        /// IP
        /// </summary>	
        [StringLength(30, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "IP")]
        public string Ip { get; set; }


        /// <summary>
        /// 异常页面
        /// </summary>	
        [StringLength(100, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "异常页面")]
        public string PageUrl { get; set; }


        /// <summary>
        /// 异常消息
        /// </summary>	
        [Display(Name = "异常消息")]
        public string ErrMessage { get; set; }


        /// <summary>
        /// 异常源
        /// </summary>	
        [Display(Name = "异常源")]
        public string ErrSource { get; set; }


        /// <summary>
        /// 堆栈轨迹
        /// </summary>	
        [Display(Name = "堆栈轨迹")]
        public string StackTrace { get; set; }


        /// <summary>
        /// 帮助连接
        /// </summary>	
        [StringLength(50, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "帮助连接")]
        public string HelpLink { get; set; }


        /// <summary>
        /// 错误类型，0=后台，1=前台，......

        /// </summary>	
        [Display(Name = "错误类型，0=后台，1=前台，...... ")]
        public int Type { get; set; }


        /// <summary>
        /// IsDel
        /// </summary>	
        [Display(Name = "IsDel")]
        public int IsDel { get; set; }   
    }
}


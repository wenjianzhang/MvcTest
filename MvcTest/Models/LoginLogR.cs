using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MvcTest.Models
{
    //登陆日志表
    public class LoginLogR
    {

        /// <summary>
        /// 登录日志Id
        /// </summary>	
        [Required]
        [Display(Name = "登录日志Id")]
        public int LoginLogID { get; set; }

        /// <summary>
        /// 登录地点
        /// </summary>	
        [Required]
        [Display(Name = "登录地点")]
        public int City { get; set; }

        /// <summary>
        /// 登陆日期
        /// </summary>	
        [Required]
        [Display(Name = "登陆日期")]
        public DateTime AddDate { get; set; }


        /// <summary>
        /// 登陆用户ID
        /// </summary>	
        [Required]
        [Display(Name = "登陆用户ID")]
        public int Manager_Id { get; set; }


        /// <summary>
        /// 用户中文名称
        /// </summary>	
        [StringLength(20, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "用户中文名称")]
        public string Manager_CName { get; set; }


        /// <summary>
        /// 登陆IP
        /// </summary>	
        [StringLength(30, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "登陆IP")]
        public string Ip { get; set; }


        /// <summary>
        /// 操作内容
        /// </summary>	
        [StringLength(200, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "操作内容")]
        public string Notes { get; set; }


        /// <summary>
        /// IsDel
        /// </summary>	
        [Display(Name = "IsDel")]
        public int IsDel { get; set; }
    }
}


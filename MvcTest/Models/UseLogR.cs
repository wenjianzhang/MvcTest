using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MvcTest.Models
{
    //系统操作日志表
    public class UseLogR
    {

        /// <summary>
        /// 主键Id
        /// </summary>	
        [Required]
        [Display(Name = "主键Id")]
        public int Id { get; set; }


        /// <summary>
        /// 操作时间
        /// </summary>	
        [Required]
        [Display(Name = "操作时间")]
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
        /// 菜单ID（用户操作页面）
        /// </summary>	
        [Required]
        [Display(Name = "菜单ID（用户操作页面）")]
        public int MenuInfo_Id { get; set; }


        /// <summary>
        /// 菜单名称或各个页面功能名称
        /// </summary>	
        [StringLength(50, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "菜单名称或各个页面功能名称")]
        public string MenuInfo_Name { get; set; }


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


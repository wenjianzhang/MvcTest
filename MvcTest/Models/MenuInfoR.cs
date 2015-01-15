using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MvcTest.Models
{
    //菜单表
    public class MenuInfoR
    {

        /// <summary>
        /// 菜单Id
        /// </summary>	
        [Required]
        [Display(Name = "菜单Id")]
        public int MIID { get; set; }


        /// <summary>
        /// 菜单名称或各个页面功能名称
        /// </summary>	
        [StringLength(50, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "菜单名称或各个页面功能名称")]
        public string Name { get; set; }


        /// <summary>
        /// 各页面URL（主菜单与分类菜单没有URL）
        /// </summary>	
        [StringLength(250, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "各页面URL（主菜单与分类菜单没有URL）")]
        public string Url { get; set; }


        /// <summary>
        /// 父ID
        /// </summary>	
        [Required]
        [Display(Name = "父ID")]
        public string ParentId { get; set; }


        /// <summary>
        /// 排序
        /// </summary>	
        [Required]
        [Display(Name = "排序")]
        public int Sort { get; set; }


        /// <summary>
        /// 深度
        /// </summary>	
        [Required]
        [Display(Name = "深度")]
        public int Depth { get; set; }


        /// <summary>
        /// 该菜单是否在菜单栏显示
        /// </summary>	
        [Required]
        [Display(Name = "该菜单是否在菜单栏显示")]
        public int IsDisplay { get; set; }


        /// <summary>
        /// 是否是菜单还是页面
        /// </summary>	
        [Required]
        [Display(Name = "是否是菜单还是页面")]
        public int IsMenu { get; set; }


        /// <summary>
        /// IsDel
        /// </summary>	
        [Display(Name = "IsDel")]
        public int IsDel { get; set; }   
    }
}


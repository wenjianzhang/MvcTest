using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MvcTest.Models
{
    //公用页面权限标志表
    public class PagePowerSignPublicR
    {

        /// <summary>
        /// 主键Id
        /// </summary>	
        [Required]
        [Display(Name = "主键Id")]
        public int PPSPID { get; set; }


        /// <summary>
        /// 权限名称
        /// </summary>	
        [StringLength(20, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "权限名称")]
        public string CName { get; set; }


        /// <summary>
        /// 权限英文名称
        /// </summary>	
        [StringLength(50, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "权限英文名称")]
        public string EName { get; set; }


        /// <summary>
        /// Class样式
        /// </summary>	
        [StringLength(50, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "Class样式")]
        public string StyleClass { get; set; }


        /// <summary>
        /// 类型
        /// </summary>	
        [StringLength(50, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "类型")]
        public string Type { get; set; }


        /// <summary>
        /// IsDel
        /// </summary>	
        [Display(Name = "IsDel")]
        public int IsDel { get; set; }   
    }
}


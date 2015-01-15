using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MvcTest.Models
{
    //部门表
    public class BranchR
    {

        /// <summary>
        /// 部门主键Id
        /// </summary>	
        [Required]
        [Display(Name = "部门主键Id")]
        public int BID { get; set; }


        /// <summary>
        /// 部门ID
        /// </summary>	
        [StringLength(50, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "部门ID")]
        public string Code { get; set; }


        /// <summary>
        /// 部门名称
        /// </summary>	
        [StringLength(25, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "部门名称")]
        public string Name { get; set; }


        /// <summary>
        /// 备注
        /// </summary>	
        [StringLength(100, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "备注")]
        public string Notes { get; set; }


        /// <summary>
        /// 父ID
        /// </summary>	
        [Required]
        [Display(Name = "父ID")]
        public int ParentId { get; set; }


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
        /// 修改人员id
        /// </summary>	
        [Required]
        [Display(Name = "修改人员id")]
        public int Manager_Id { get; set; }


        /// <summary>
        /// 修改人员姓名
        /// </summary>	
        [StringLength(20, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "修改人员姓名")]
        public string Manager_CName { get; set; }


        /// <summary>
        /// 修改时间
        /// </summary>	
        [Required]
        [Display(Name = "修改时间")]
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// IsDel
        /// </summary>	
        [Display(Name = "IsDel")]
        public int IsDel { get; set; }   

    }
}


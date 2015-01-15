using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MvcTest.Models
{
    //职位表
    public class PositionR
    {

        /// <summary>
        /// 职位主键Id
        /// </summary>	
        [Required]
        [Display(Name = "职位主键Id")]
        public int PID { get; set; }


        /// <summary>
        /// 职位名称
        /// </summary>	
        [StringLength(30, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "职位名称")]
        public string Name { get; set; }


        /// <summary>
        /// 部门自编号ID
        /// </summary>	
        [Required]
        [Display(Name = "部门自编号ID")]
        public int Branch_Id { get; set; }


        /// <summary>
        /// 部门编号
        /// </summary>	
        [StringLength(50, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "部门编号")]
        public string Branch_Code { get; set; }


        /// <summary>
        /// 部门名称
        /// </summary>	
        [StringLength(25, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "部门名称")]
        public string Branch_Name { get; set; }


        /// <summary>
        /// 菜单操作权限，有操作权限的菜单ID列表：|1|2|3|4|5|

        /// </summary>	
        [Display(Name = "菜单操作权限，有操作权限的菜单ID列表：|1|2|3|4|5| ")]
        public string PagePower { get; set; }


        /// <summary>
        /// 页面功能操作权限，各个页面有操作权限的菜单ID和页面权限标志ID列表：|1,1|2,1|2,2|2,4|

        /// </summary>	
        [Display(Name = "页面功能操作权限，各个页面有操作权限的菜单ID和页面权限标志ID列表：|1,1|2,1|2,2|2,4| ")]
        public string ControlPower { get; set; }


        /// <summary>
        /// 是否有操作绑定指定部门的权限，0=无，1=有

        /// </summary>	
        [Required]
        [Display(Name = "是否有操作绑定指定部门的权限，0=无，1=有 ")]
        public int IsSetBranchPower { get; set; }


        /// <summary>
        /// 绑定部门的编号

        /// </summary>	
        [StringLength(50, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "绑定部门的编号 ")]
        public string SetBranchCode { get; set; }


        /// <summary>
        /// 修改人员id

        /// </summary>	
        [Required]
        [Display(Name = "修改人员id ")]
        public int Manager_Id { get; set; }


        /// <summary>
        /// 修改人员姓名

        /// </summary>	
        [StringLength(20, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "修改人员姓名 ")]
        public string Manager_CName { get; set; }


        /// <summary>
        /// 修改时间

        /// </summary>	
        [Required]
        [Display(Name = "修改时间 ")]
        public DateTime UpdateDate { get; set; }


        /// <summary>
        /// IsDel
        /// </summary>	
        [Display(Name = "IsDel")]
        public int IsDel { get; set; }   
    }
}


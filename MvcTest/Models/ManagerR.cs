using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MvcTest.Models
{
    //管理员表
    public class ManagerR
    {

        /// <summary>
        /// 管理员表ID
        /// </summary>	
        [Required]
        [Display(Name = "管理员表ID")]
        public int MID { get; set; }


        /// <summary>
        /// 登陆账号
        /// </summary>	
        [StringLength(20, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "登陆账号")]
        public string LoginName { get; set; }


        /// <summary>
        /// 登陆密码
        /// </summary>	
        [StringLength(32, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "登陆密码")]
        public string LoginPass { get; set; }


        /// <summary>
        /// 最后登陆时间
        /// </summary>	
        [Display(Name = "最后登陆时间")]
        public DateTime LoginTime { get; set; }


        /// <summary>
        /// 最后登陆IP
        /// </summary>	
        [StringLength(30, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "最后登陆IP")]
        public string LoginIp { get; set; }


        /// <summary>
        /// 登陆次数
        /// </summary>	
        [Required]
        [Display(Name = "登陆次数")]
        public int LoginCount { get; set; }


        /// <summary>
        /// 注册时间
        /// </summary>	
        [Required]
        [Display(Name = "注册时间")]
        public DateTime CreateTime { get; set; }


        /// <summary>
        /// 资料最后修改日期
        /// </summary>	
        [Required]
        [Display(Name = "资料最后修改日期")]
        public DateTime UpdateTime { get; set; }


        /// <summary>
        /// 所属部门ID
        /// </summary>	
        [Required]
        [Display(Name = "所属部门ID")]
        public int Branch_Id { get; set; }


        /// <summary>
        /// 所属部门编号，用户只能正式归属于一个部门

        /// </summary>	
        [StringLength(20, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "所属部门编号，用户只能正式归属于一个部门 ")]
        public string Branch_Code { get; set; }


        /// <summary>
        /// 部门名称
        /// </summary>	
        [StringLength(25, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "部门名称")]
        public string Branch_Name { get; set; }


        /// <summary>
        /// 用户职位ID
        /// </summary>	
        [Required]
        [Display(Name = "用户职位ID")]
        public int Position_Id { get; set; }


        /// <summary>
        /// 职位名称
        /// </summary>	
        [StringLength(30, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "职位名称")]
        public string Position_Name { get; set; }


        /// <summary>
        /// 账号是否启用，1=true(启用)，0=false（禁用）

        /// </summary>	
        [Required]
        [Display(Name = "账号是否启用，1=true(启用)，0=false（禁用）")]
        public int Enable { get; set; }


        /// <summary>
        /// 用户中文名称
        /// </summary>	
        [StringLength(20, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "用户中文名称")]
        public string CName { get; set; }


        /// <summary>
        /// 用户英文名称
        /// </summary>	
        [StringLength(50, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "用户英文名称")]
        public string EName { get; set; }


        /// <summary>
        /// 头像图片路径
        /// </summary>	
        [StringLength(250, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "头像图片路径")]
        public string PhotoImg { get; set; }


        /// <summary>
        /// 性别（0=未知，1=男，2=女）

        /// </summary>	
        [StringLength(4, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "性别（0=未知，1=男，2=女）")]
        public string Sex { get; set; }


        /// <summary>
        /// 出生日期
        /// </summary>	
        [StringLength(20, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "出生日期")]
        public string Birthday { get; set; }


        /// <summary>
        /// 籍贯
        /// </summary>	
        [StringLength(100, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "籍贯")]
        public string NativePlace { get; set; }


        /// <summary>
        /// 民族
        /// </summary>	
        [StringLength(50, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "民族")]
        public string NationalName { get; set; }


        /// <summary>
        /// 个人--学历
        /// </summary>	
        [StringLength(25, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "个人--学历")]
        public string Record { get; set; }


        /// <summary>
        /// 毕业学校
        /// </summary>	
        [StringLength(30, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "毕业学校")]
        public string GraduateCollege { get; set; }


        /// <summary>
        /// 毕业专业
        /// </summary>	
        [StringLength(50, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "毕业专业")]
        public string GraduateSpecialty { get; set; }


        /// <summary>
        /// 个人--联系电话
        /// </summary>	
        [StringLength(30, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "个人--联系电话")]
        public string Tel { get; set; }


        /// <summary>
        /// 个人--移动电话
        /// </summary>	
        [StringLength(30, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "个人--移动电话")]
        public string Mobile { get; set; }


        /// <summary>
        /// 个人--联系邮箱
        /// </summary>	
        [StringLength(50, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "个人--联系邮箱")]
        public string Email { get; set; }


        /// <summary>
        /// 个人--QQ
        /// </summary>	
        [StringLength(30, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "个人--QQ")]
        public string Qq { get; set; }


        /// <summary>
        /// 个人--Msn
        /// </summary>	
        [StringLength(30, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "个人--Msn")]
        public string Msn { get; set; }


        /// <summary>
        /// 个人--通讯地址
        /// </summary>	
        [StringLength(100, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "个人--通讯地址")]
        public string Address { get; set; }


        /// <summary>
        /// 备注
        /// </summary>	
        [Display(Name = "备注")]
        public string Content { get; set; }


        /// <summary>
        /// 修改人员id
        /// </summary>	
        [Required]
        [Display(Name = "修改人员id")]
        public int Manager_Id { get; set; }


        /// <summary>
        /// 修改人中文名称
        /// </summary>	
        [StringLength(20, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "修改人中文名称")]
        public string Manager_CName { get; set; }


        /// <summary>
        /// IsDel
        /// </summary>	
        [Display(Name = "IsDel")]
        public int IsDel { get; set; }   
    }
}


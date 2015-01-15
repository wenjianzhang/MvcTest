using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcTest.Models
{
    //C_Channel
    public class C_ChannelR
    {

        /// <summary>
        /// 自动编号
        /// </summary>	
        [Required]
        [Display(Name = "自动编号")]
        public int ChannelID { get; set; }


        /// <summary>
        /// 唯一编号
        /// </summary>	
        [Display(Name = "唯一编号")]
        public string CID { get; set; }


        /// <summary>
        /// 频道名称
        /// </summary>	
        [Display(Name = "频道名称")]
        public string ChannelName { get; set; }


        /// <summary>
        /// 频道英文
        /// </summary>	
        [Display(Name = "频道英文")]
        public string ChannelEName { get; set; }


        /// <summary>
        /// 父频道
        /// </summary>	
        [Display(Name = "父频道")]
        public string PCID { get; set; }


        /// <summary>
        /// 是否外部栏目
        /// </summary>	
        [Display(Name = "是否外部栏目")]
        public int IsOutChannel { get; set; }


        /// <summary>
        /// 外部栏目地址
        /// </summary>	
        [Display(Name = "外部栏目地址")]
        public string OutChannelUrl { get; set; }


        /// <summary>
        /// 排序
        /// </summary>	
        [Display(Name = "排序")]
        public int Sorc { get; set; }


        /// <summary>
        /// 绑定域名
        /// </summary>	
        [Display(Name = "绑定域名")]
        public string BindDomain { get; set; }


        /// <summary>
        /// 审核机制
        /// </summary>	
        [Display(Name = "审核机制")]
        public string Review { get; set; }


        /// <summary>
        /// IsDel
        /// </summary>	
        [Display(Name = "IsDel")]
        public int IsDel { get; set; }


        /// <summary>
        /// Meta关键字
        /// </summary>	
        [Display(Name = "Meta关键字")]
        public string MetaKeyword { get; set; }


        /// <summary>
        /// Meta描述
        /// </summary>	
        [Display(Name = "Meta描述")]
        public string MetaDescription { get; set; }
    }
}
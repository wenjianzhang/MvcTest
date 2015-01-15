using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcTest.Models
{
    //C_Class
    public class C_ClassR
    {

        /// <summary>
        /// 自动编号
        /// </summary>	
        [Required]
        [Display(Name = "自动编号")]
        public int ClassID { get; set; }


        /// <summary>
        /// 唯一编号
        /// </summary>	
        [Display(Name = "唯一编号")]
        public string CID { get; set; }


        /// <summary>
        /// 栏目名称
        /// </summary>	
        [Display(Name = "栏目名称")]
        public string ClassName { get; set; }


        /// <summary>
        /// 栏目英文
        /// </summary>	
        [Display(Name = "栏目英文")]
        public string ClassEName { get; set; }


        /// <summary>
        /// 父栏目
        /// </summary>	
        [Display(Name = "父栏目")]
        public string PCID { get; set; }


        /// <summary>
        /// 是否外部栏目
        /// </summary>	
        [Display(Name = "是否外部栏目")]
        public int IsOutClass { get; set; }


        /// <summary>
        /// 外部地址
        /// </summary>	
        [Display(Name = "外部地址")]
        public string OutClassUrl { get; set; }


        /// <summary>
        /// 排序
        /// </summary>	
        [Display(Name = "排序")]
        public int Sort { get; set; }


        /// <summary>
        /// 捆绑域名
        /// </summary>	
        [Display(Name = "捆绑域名")]
        public string BindDomain { get; set; }


        /// <summary>
        /// 栏目模板
        /// </summary>	
        [Display(Name = "栏目模板")]
        public string ClassModel { get; set; }


        /// <summary>
        /// 保存路径
        /// </summary>	
        [Display(Name = "保存路径")]
        public string Saveurl { get; set; }


        /// <summary>
        /// 保存栏目生成目录
        /// </summary>	
        [Display(Name = "保存栏目生成目录")]
        public string ClassFile { get; set; }


        /// <summary>
        /// 审核机制
        /// </summary>	
        [Display(Name = "审核机制")]
        public string Review { get; set; }


        /// <summary>
        /// 命名规则
        /// </summary>	
        [Display(Name = "命名规则")]
        public string Namerules { get; set; }


        /// <summary>
        /// 每天生成一个索引页
        /// </summary>	
        [Display(Name = "每天生成一个索引页")]
        public string PageIndex { get; set; }


        /// <summary>
        /// 新闻保存目录
        /// </summary>	
        [Display(Name = "新闻保存目录")]
        public string NewsSaveurl { get; set; }


        /// <summary>
        /// 新闻浏览文件名规则
        /// </summary>	
        [Display(Name = "新闻浏览文件名规则")]
        public string NewsLookName { get; set; }


        /// <summary>
        /// 图片上传目录
        /// </summary>	
        [Display(Name = "图片上传目录")]
        public string ImageUploadurl { get; set; }


        /// <summary>
        /// 发布时间
        /// </summary>	
        [Display(Name = "发布时间")]
        public DateTime PublicTime { get; set; }


        /// <summary>
        /// 栏目默认数据表
        /// </summary>	
        [Display(Name = "栏目默认数据表")]
        public string ClassDataTable { get; set; }


        /// <summary>
        /// 频道id
        /// </summary>	
        [Display(Name = "频道id")]
        public string ChannelID { get; set; }


        /// <summary>
        /// 在导航中显示
        /// </summary>	
        [Display(Name = "在导航中显示")]
        public string IsMenuShow { get; set; }


        /// <summary>
        /// 导航图片
        /// </summary>	
        [Display(Name = "导航图片")]
        public string MenuImage { get; set; }


        /// <summary>
        /// 导航文字
        /// </summary>	
        [Display(Name = "导航文字")]
        public string MenuTitle { get; set; }


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


        /// <summary>
        /// 生成文件扩展名
        /// </summary>	
        [Display(Name = "生成文件扩展名")]
        public string SCFileEXT { get; set; }


        /// <summary>
        /// 是否锁定
        /// </summary>	
        [Display(Name = "是否锁定")]
        public int IsLock { get; set; }


        /// <summary>
        /// 是否在回收站
        /// </summary>	
        [Display(Name = "是否在回收站")]
        public int IsDel { get; set; }


        /// <summary>
        /// 自定义编号
        /// </summary>	
        [Display(Name = "自定义编号")]
        public int CustomCode { get; set; }


        /// <summary>
        /// 创建日期
        /// </summary>	
        [Display(Name = "创建日期")]
        public DateTime CreateTime { get; set; }


        /// <summary>
        /// IsPage
        /// </summary>	
        [Display(Name = "IsPage")]
        public int IsPage { get; set; }


        /// <summary>
        /// 是否生成静态文件
        /// </summary>	
        [Display(Name = "是否生成静态文件")]
        public int IsHtml { get; set; }


    }
}


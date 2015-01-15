using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcTest.Models
{
    public class C_NewsR
    {

        /// <summary>
        /// 自动编号
        /// </summary>	
        [Required]
        [Display(Name = "自动编号")]
        public int NID { get; set; }


        /// <summary>
        /// 唯一编号
        /// </summary>	
        [Display(Name = "唯一编号")]
        public string NewsID { get; set; }


        /// <summary>
        /// 新闻类型：0 普通、1 图片、2 标题
        /// </summary>	
        [Display(Name = "新闻类型：0 普通、1 图片、2 标题")]
        public string ChannelID { get; set; }


        /// <summary>
        /// ClassID
        /// </summary>	
        [Display(Name = "ClassID")]
        public string ClassID { get; set; }


        /// <summary>
        /// 新闻权重：1-50的数字。0 置顶。数字越小，权重越高
        /// </summary>	
        [Display(Name = "新闻权重：1-50的数字。0 置顶。数字越小，权重越高")]
        public int OrderID { get; set; }


        /// <summary>
        /// 标题
        /// </summary>	
        [Display(Name = "标题")]
        public string NewsTitle { get; set; }


        /// <summary>
        /// 副标题
        /// </summary>	
        [Display(Name = "副标题")]
        public string SNewsTitle { get; set; }


        /// <summary>
        /// 标题颜色
        /// </summary>	
        [Display(Name = "标题颜色")]
        public string TitleColor { get; set; }


        /// <summary>
        /// 是否斜体
        /// </summary>	
        [Display(Name = "是否斜体")]
        public int TitleITF { get; set; }


        /// <summary>
        /// 是否粗体
        /// </summary>	
        [Display(Name = "是否粗体")]
        public int TitleBIT { get; set; }


        /// <summary>
        /// 链接地址
        /// </summary>	
        [Display(Name = "链接地址")]
        public string URLLaddress { get; set; }


        /// <summary>
        /// 图片地址（大）
        /// </summary>	
        [Display(Name = "图片地址（大）")]
        public string PicURL { get; set; }


        /// <summary>
        /// 图片地址（小）自动生成的
        /// </summary>	
        [Display(Name = "图片地址（小）自动生成的")]
        public string inPicURL { get; set; }


        /// <summary>
        /// 作者
        /// </summary>	
        [Display(Name = "作者")]
        public string Auther { get; set; }


        /// <summary>
        /// 来源
        /// </summary>	
        [Display(Name = "来源")]
        public string Souce { get; set; }


        /// <summary>
        /// Tags多个用，分开
        /// </summary>	
        [Display(Name = "Tags多个用，分开")]
        public string Tags { get; set; }


        /// <summary>
        /// 新闻属性 推荐、滚动、热点、幻灯、头条（头条可以直接生成图片头条）、公告、WAP、精彩  格式：0，1，1，0，1，0，0，1
        /// </summary>	
        [Display(Name = "新闻属性  推荐、滚动、热点、幻灯、头条（头条可以直接生成图片头条）、公告、WAP、精彩  格式：0，1，1，0，1，0，0，1")]
        public int NewsProperty { get; set; }


        /// <summary>
        /// 是否图片头条
        /// </summary>	
        [Display(Name = "是否图片头条")]
        public int NewsPicTopline { get; set; }


        /// <summary>
        /// 模板
        /// </summary>	
        [Display(Name = "模板")]
        public string Templet { get; set; }


        /// <summary>
        /// 内容
        /// </summary>	
        [Display(Name = "内容")]
        public string Content { get; set; }


        /// <summary>
        /// meta关键字
        /// </summary>	
        [Display(Name = "meta关键字")]
        public string MetaKeywords { get; set; }


        /// <summary>
        /// meta描述
        /// </summary>	
        [Display(Name = "meta描述")]
        public string Metadesc { get; set; }


        /// <summary>
        /// Click
        /// </summary>	
        [Display(Name = "Click")]
        public int Click { get; set; }


        /// <summary>
        /// CreateTime
        /// </summary>	
        [Display(Name = "CreateTime")]
        public DateTime CreateTime { get; set; }


        /// <summary>
        /// 编辑时间
        /// </summary>	
        [Display(Name = "编辑时间")]
        public DateTime UpdateTime { get; set; }


        /// <summary>
        /// PublicTime
        /// </summary>	
        [Display(Name = "PublicTime")]
        public string PublicTime { get; set; }


        /// <summary>
        /// 保存路径
        /// </summary>	
        [Display(Name = "保存路径")]
        public string SavePath { get; set; }


        /// <summary>
        /// 文件名
        /// </summary>	
        [Display(Name = "文件名")]
        public string FileName { get; set; }


        /// <summary>
        /// 扩展名
        /// </summary>	
        [Display(Name = "扩展名")]
        public string FileEXName { get; set; }


        /// <summary>
        /// 被顶次数
        /// </summary>	
        [Display(Name = "被顶次数")]
        public int TopNum { get; set; }


        /// <summary>
        /// 是否锁定
        /// </summary>	
        [Display(Name = "是否锁定")]
        public int IsLock { get; set; }


        /// <summary>
        /// 是否回收站
        /// </summary>	
        [Display(Name = "是否回收站")]
        public int IsDel { get; set; }


        /// <summary>
        /// 使用数据库表
        /// </summary>	
        [Display(Name = "使用数据库表")]
        public string DataLib { get; set; }


        /// <summary>
        /// 自定义数据编号
        /// </summary>	
        [Display(Name = "自定义数据编号")]
        public string DefineID { get; set; }


        /// <summary>
        /// 是否生成HTML
        /// </summary>	
        [Display(Name = "是否生成HTML")]
        public int IsHTML { get; set; }


        /// <summary>
        /// 状态
        /// </summary>	
        [Display(Name = "状态")]
        public int CheckStat { get; set; }


        /// <summary>
        /// 排序
        /// </summary>	
        [Display(Name = "排序")]
        public int Sorc { get; set; }


    }
}


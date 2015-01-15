using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcTest.Models
{
    //NewsTab
    public class Spiders_NewsR
    {
        /// <summary>
        /// Id
        /// </summary>	
        [Required]
        [Display(Name = "Id")]
        public long Id { get; set; }


        /// <summary>
        /// SourceSiteTag
        /// </summary>	
        [Display(Name = "SourceSiteTag")]
        public string SourceSiteTag { get; set; }


        /// <summary>
        /// SourceSiteName
        /// </summary>	
        [Display(Name = "SourceSiteName")]
        public string SourceSiteName { get; set; }


        /// <summary>
        /// Title
        /// </summary>	
        [Display(Name = "Title")]
        public string Title { get; set; }


        /// <summary>
        /// TypeName
        /// </summary>	
        [Display(Name = "TypeName")]
        public string TypeName { get; set; }


        /// <summary>
        /// NewPublishTime
        /// </summary>	
        [Display(Name = "NewPublishTime")]
        public DateTime NewPublishTime { get; set; }


        /// <summary>
        /// BaseUrl
        /// </summary>	
        [Display(Name = "BaseUrl")]
        public string BaseUrl { get; set; }


        /// <summary>
        /// Url
        /// </summary>	
        [Display(Name = "Url")]
        public string Url { get; set; }


        /// <summary>
        /// ImgUrl
        /// </summary>	
        [Display(Name = "ImgUrl")]
        public string ImgUrl { get; set; }


        /// <summary>
        /// Content
        /// </summary>	
        [Display(Name = "Content")]
        public string Content { get; set; }


        /// <summary>
        /// CreateTime
        /// </summary>	
        [Display(Name = "CreateTime")]
        public DateTime CreateTime { get; set; }


        /// <summary>
        /// IsDel
        /// </summary>	
        [Display(Name = "IsDel")]
        public int IsDel { get; set; }



    }
}


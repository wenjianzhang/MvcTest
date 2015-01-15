using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcTest.Models
{
    //CollectionRulesTab
    public class Spiders_CollectionRulesR
    {
        /// <summary>
        /// Id
        /// </summary>	
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; }


        /// <summary>
        /// CollectionName
        /// </summary>	
        [Display(Name = "CollectionName")]
        public string CollectionName { get; set; }


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
        /// BaseUrl
        /// </summary>	
        [Display(Name = "BaseUrl")]
        public string BaseUrl { get; set; }


        /// <summary>
        /// CollectionListRulesBig
        /// </summary>	
        [Display(Name = "CollectionListRulesBig")]
        public string CollectionListRulesBig { get; set; }

        /// <summary>
        /// IsDel
        /// </summary>	
        [Display(Name = "IsDel")]
        public int IsDel { get; set; }

        /// <summary>
        /// CollectionListRulesMin
        /// </summary>	
        [Display(Name = "CollectionListRulesMin")]
        public string CollectionListRulesMin { get; set; }


        /// <summary>
        /// CollectionUrl
        /// </summary>	
        [Display(Name = "CollectionUrl")]
        public string CollectionUrl { get; set; }


        /// <summary>
        /// Contentleft
        /// </summary>	
        [Display(Name = "Contentleft")]
        public string Contentleft { get; set; }


        /// <summary>
        /// Contentright
        /// </summary>	
        [Display(Name = "Contentright")]
        public string Contentright { get; set; }


        /// <summary>
        /// Titleleft
        /// </summary>	
        [Display(Name = "Titleleft")]
        public string Titleleft { get; set; }


        /// <summary>
        /// Titleright
        /// </summary>	
        [Display(Name = "Titleright")]
        public string Titleright { get; set; }


        /// <summary>
        /// Imgleft
        /// </summary>	
        [Display(Name = "Imgleft")]
        public string Imgleft { get; set; }


        /// <summary>
        /// Imgright
        /// </summary>	
        [Display(Name = "Imgright")]
        public string Imgright { get; set; }


        /// <summary>
        /// NewPublishTimeLeft
        /// </summary>	
        [Display(Name = "NewPublishTimeLeft")]
        public string NewPublishTimeLeft { get; set; }


        /// <summary>
        /// NewPublishTimeRight
        /// </summary>	
        [Display(Name = "NewPublishTimeRight")]
        public string NewPublishTimeRight { get; set; }


        /// <summary>
        /// TypeNameLeft
        /// </summary>	
        [Display(Name = "TypeNameLeft")]
        public string TypeNameLeft { get; set; }


        /// <summary>
        /// TypeNameRight
        /// </summary>	
        [Display(Name = "TypeNameRight")]
        public string TypeNameRight { get; set; }


        /// <summary>
        /// Encoding
        /// </summary>	
        [Display(Name = "Encoding")]
        public string Encoding { get; set; }



    }
}


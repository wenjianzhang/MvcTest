using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MvcTest.Models
{
    //角色菜单页面权限表
    public class RoleMenuPageR
    {

        /// <summary>
        /// id
        /// </summary>	
        [Required]
        [Display(Name = "id")]
        public int id { get; set; }


        /// <summary>
        /// MIID
        /// </summary>	
        [Display(Name = "MIID")]
        public int MIID { get; set; }


        /// <summary>
        /// PPSID
        /// </summary>	
        [Display(Name = "PPSID")]
        public int PPSID { get; set; }


        /// <summary>
        /// RID
        /// </summary>	
        [Display(Name = "RID")]
        public int RID { get; set; }



    }
}


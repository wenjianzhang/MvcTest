using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MvcTest.Models
{
    //RoleManager
    public class RoleManagerR
    {

        /// <summary>
        /// ID
        /// </summary>	
        [Required]
        [Display(Name = "ID")]
        public int ID { get; set; }


        /// <summary>
        /// RID
        /// </summary>	
        [Display(Name = "RID")]
        public int RID { get; set; }


        /// <summary>
        /// 管理员表ID
        /// </summary>	
        [Display(Name = "管理员表ID")]
        public int MID { get; set; }



    }
}


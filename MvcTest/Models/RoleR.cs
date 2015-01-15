using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MvcTest.Models
{
    //角色表
    public class RoleR
    {

        /// <summary>
        /// RID
        /// </summary>	
        [Required]
        [Display(Name = "RID")]
        public int RID { get; set; }


        /// <summary>
        /// Name
        /// </summary>	
        [StringLength(20, ErrorMessage = "  超出了字符的最大长度 {0} 个字符。")]
        [Display(Name = "角色名称")]
        public string Name { get; set; }


        /// <summary>
        /// IsDel
        /// </summary>	
        [Display(Name = "IsDel")]
        public int IsDel { get; set; }   
    }
}


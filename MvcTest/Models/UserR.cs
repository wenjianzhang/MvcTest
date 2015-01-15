using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MvcTest.Model
{
    //User
    public class ThirdUserR
    {

        /// <summary>
        /// Id
        /// </summary>	
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; }


        /// <summary>
        /// UserName
        /// </summary>	
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }


        /// <summary>
        /// PassWord
        /// </summary>	
        [Required]
        [Display(Name = "PassWord")]
        public string PassWord { get; set; }


        /// <summary>
        /// CreatTime
        /// </summary>	
        [Display(Name = "CreatTime")]
        public DateTime CreatTime { get; set; }


        /// <summary>
        /// LastTime
        /// </summary>	
        [Display(Name = "LastTime")]
        public DateTime LastTime { get; set; }


        /// <summary>
        /// ThirdType
        /// </summary>	
        [Display(Name = "ThirdType")]
        public string ThirdType { get; set; }


        /// <summary>
        /// ThirdId
        /// </summary>	
        [Display(Name = "ThirdId")]
        public string ThirdId { get; set; }


        /// <summary>
        /// Access_token
        /// </summary>	
        [Display(Name = "Access_token")]
        public string Access_token { get; set; }


        /// <summary>
        /// NiceName
        /// </summary>	
        [Display(Name = "NiceName")]
        public string NiceName { get; set; }


        /// <summary>
        /// Sex
        /// </summary>	
        [Display(Name = "Sex")]
        public string Sex { get; set; }


        /// <summary>
        /// Province
        /// </summary>	
        [Display(Name = "Province")]
        public string Province { get; set; }


        /// <summary>
        /// City
        /// </summary>	
        [Display(Name = "City")]
        public string City { get; set; }


        /// <summary>
        /// Photo
        /// </summary>	
        [Display(Name = "Photo")]
        public string Photo { get; set; }


        /// <summary>
        /// Expires_in
        /// </summary>	
        [Display(Name = "Expires_in")]
        public string Expires_in { get; set; }
    }
}


//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class ThirdUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public Nullable<System.DateTime> CreatTime { get; set; }
        public Nullable<System.DateTime> LastTime { get; set; }
        public string ThirdType { get; set; }
        public string ThirdId { get; set; }
        public string Access_token { get; set; }
        public string NiceName { get; set; }
        public string Sex { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Photo { get; set; }
        public string Expires_in { get; set; }
        public Nullable<int> IsDel { get; set; }
    }
}

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
    
    public partial class Position
    {
        public int PID { get; set; }
        public string Name { get; set; }
        public int Branch_Id { get; set; }
        public string Branch_Code { get; set; }
        public string Branch_Name { get; set; }
        public string PagePower { get; set; }
        public string ControlPower { get; set; }
        public byte IsSetBranchPower { get; set; }
        public string SetBranchCode { get; set; }
        public int Manager_Id { get; set; }
        public string Manager_CName { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public Nullable<int> IsDel { get; set; }
    }
}

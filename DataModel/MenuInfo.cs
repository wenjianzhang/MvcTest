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
    
    public partial class MenuInfo
    {
        public MenuInfo()
        {
            this.PagePowerSigns = new HashSet<PagePowerSign>();
        }
    
        public int MIID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int ParentId { get; set; }
        public int Sort { get; set; }
        public int Depth { get; set; }
        public byte IsDisplay { get; set; }
        public byte IsMenu { get; set; }
        public Nullable<int> IsDel { get; set; }
    
        public virtual ICollection<PagePowerSign> PagePowerSigns { get; set; }
    }
}

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
    
    public partial class C_News
    {
        public int NID { get; set; }
        public string NewsID { get; set; }
        public Nullable<int> ChannelID { get; set; }
        public Nullable<int> ClassID { get; set; }
        public Nullable<int> OrderID { get; set; }
        public string NewsTitle { get; set; }
        public string SNewsTitle { get; set; }
        public string TitleColor { get; set; }
        public Nullable<int> TitleITF { get; set; }
        public Nullable<int> TitleBIT { get; set; }
        public string URLLaddress { get; set; }
        public string PicURL { get; set; }
        public string inPicURL { get; set; }
        public string Auther { get; set; }
        public string Souce { get; set; }
        public string Tags { get; set; }
        public Nullable<int> NewsProperty { get; set; }
        public Nullable<int> NewsPicTopline { get; set; }
        public string Templet { get; set; }
        public string Content { get; set; }
        public string MetaKeywords { get; set; }
        public string Metadesc { get; set; }
        public Nullable<int> Click { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public string SavePath { get; set; }
        public string FileName { get; set; }
        public string FileEXName { get; set; }
        public Nullable<int> TopNum { get; set; }
        public Nullable<int> IsLock { get; set; }
        public Nullable<int> IsDel { get; set; }
        public string DataLib { get; set; }
        public string DefineID { get; set; }
        public Nullable<int> IsHTML { get; set; }
        public Nullable<int> CheckStat { get; set; }
        public Nullable<int> Sorc { get; set; }
        public Nullable<System.DateTime> PublicTime { get; set; }
    
        public virtual C_Class C_Class { get; set; }
    }
}
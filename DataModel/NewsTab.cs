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
    
    public partial class NewsTab
    {
        public long Id { get; set; }
        public string SourceSiteTag { get; set; }
        public string SourceSiteName { get; set; }
        public string Title { get; set; }
        public string TypeName { get; set; }
        public Nullable<System.DateTime> NewPublishTime { get; set; }
        public string BaseUrl { get; set; }
        public string Url { get; set; }
        public string ImgUrl { get; set; }
        public string Content { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<int> IsDel { get; set; }
    }
}

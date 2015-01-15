using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataModel.Extensions
{
    [DataContract]
    public class IpInfo
    {
        [DataMember]
        public string code { get; set; }

        [DataMember]
        public data data { set; get; }
    }

    [DataContract]
    public class data
    {
        [DataMember]
        public string country { get; set; }

        [DataMember]
        public string country_id { get; set; }

        [DataMember]
        public string area { get; set; }

        [DataMember]
        public string area_id { get; set; }

        [DataMember]
        public string region { get; set; }

        [DataMember]
        public string region_id { get; set; }

        [DataMember]
        public string city { get; set; }

        [DataMember]
        public string city_id { get; set; }

        [DataMember]
        public string county { get; set; }

        [DataMember]
        public string county_id { get; set; }

        [DataMember]
        public string isp { get; set; }

        [DataMember]
        public string isp_id { get; set; }

        [DataMember]
        public string ip { get; set; }
    }
}

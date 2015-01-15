using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DataModel.Extensions;

namespace Common.Extensions
{
    public static class Extensions
    {

        public static IpInfo GetIpCityMessage(string content, string returnStrType = "json")
        {
            using (WebClient client = new WebClient() { Encoding = Encoding.UTF8 })
            {
                var serverUrl = string.Concat("http://ip.taobao.com/service/getIpInfo.php");
                var data = new NameValueCollection();

                data.Add("ip", content);
                //data.Add("respond", returnStrType); 
                //data.Add("charset", encoding.BodyName); 
                //data.Add("ignore", "yes"); 
                //data.Add("duality", "yes"); 

                string responseContent = null;

                IpInfo createResult = null;
                try
                {
                    responseContent = Encoding.UTF8.GetString(client.UploadValues(serverUrl, data));
                    createResult = JsonConvert.DeserializeObject<IpInfo>(responseContent);

                    return createResult;
                }
                catch (Exception e)
                {
                    throw;
                }
            }

        }
    }
}

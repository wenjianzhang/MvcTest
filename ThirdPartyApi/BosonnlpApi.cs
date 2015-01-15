using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Common;
using Newtonsoft.Json;
using ThirdPartyApi.Models;
using System.Collections;
using System.Text.RegularExpressions;

namespace ThirdPartyApi
{
    public class BosonnlpApi
    {
        public List<KeywordsModel> GetKeyword(string Content, string url, string encoding, out Exception error)
        {
            List<KeywordsModel> keyword = new List<KeywordsModel>();
            String result = Regex.Replace(Content, @"<[^>]*>", String.Empty);
            using (WebClient client = new WebClient() { Encoding = (encoding == null ? Encoding.Default : Encoding.GetEncoding(encoding)) })
            {
                var serverUrl = string.Concat(Common.Configurations.BosonnlpApi.Default.KeywordsUrl);
                var data = new System.Collections.Specialized.NameValueCollection();
                //data.Add("meeting.meetingStatus", meetingRoom.Status.GetValueOrDefault(0).ToString());
                Exception Error = null;
                client.Headers.Add("X-Token", "Saorrfua.2278.23PPt6joGTQv");
                client.Headers.Set("Content-Type", "application/json");
                client.Headers.Set("Accept", "application/json");
                client.QueryString.Add("top_k", "100");
                string dataStr = "data=" + result;
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("data", result);
                string responseContent = null;
                try
                {
                    responseContent = client.UploadString(serverUrl, "post", JsonConvert.SerializeObject(result));
                    var createResult = JsonConvert.DeserializeObject<ArrayList>(responseContent);
                    //if ((createResult.flag ?? string.Empty).ToLower() != "true")
                    //    throw new Exception("失败");
                    error = Error;
                    //for (int i = 0, x = (createResult.Count > 5 ? 5 : createResult.Count); i < x; i++)
                    //{
                    foreach (var item in createResult)
                    {
                        var ss = JsonConvert.DeserializeObject<ArrayList>(item.ToString());
                        keyword.Add(new KeywordsModel { Keyword = ss[1].ToString(), Weight = ss[0].ToString() });
                    }

                    //}

                    return keyword;
                }
                catch (Exception ex)
                {
                    error = Error = ex;
                    throw;
                }
                finally
                {
                    if (Error != null)
                    {
                        Common.Loger.LogerManager.Error("自动分词 GetKeyword", Error);
                    }
                }
            }
        }
    }
}

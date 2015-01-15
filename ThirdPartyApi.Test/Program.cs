using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThirdPartyApi;
using ThirdPartyApi.Models;

namespace ThirdPartyApi.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Exception error = null;
            BosonnlpApi api = new BosonnlpApi();
            List<KeywordsModel> ss = api.GetKeyword("<!--mainContent begin--><p>原标题：俄罗斯总理呼吁俄企业家向中国学习</p> <p>新华网莫斯科9月20日电（记者刘怡然）俄罗斯总理梅德韦杰夫20日在俄南部城市索契表示，俄罗斯企业家应以中国同行为样，通过提升产品质量树立品牌声誉。</p> <p>据俄塔社报道，梅德韦杰夫当天在第十三届索契国际投资论坛上表示，他希望俄罗斯品牌能以良好的产品质量提高声誉。他说，中国过去十几年中努力提升产品质量，以更好的性价比赢得青睐，俄罗斯应该以同样的做法，让&ldquo;俄罗斯制造&rdquo;与国家形象相适应。</p> <p>同一天，梅德韦杰夫接受俄罗斯家电视台采访时表示，俄中关系具有战略意义，两国有着非常好的政治接触和经贸关系，俄方希望继续扩大合作规模，相信两国在所有领域的合作都将是平等、友好和互利的。</p><p梅德韦杰夫还表示，当前世界处于全球化当中，各经济体相互依存，相互投资，&ldquo;我们愿意看到中国投资者来到俄罗斯，俄罗斯将与中国交好，与中国发展贸易&rdquo;<span class=\"ifengLogo\"><a href=\"http://www.ifeng.com/\" target=\"_blank\"><img src=\"http://img.ifeng.com/page/Logo.gif\" width=\"15\" height=\"17\"></a></span></p><!--mainContent end-->", "", "utf-8", out error);
        }
    }
}

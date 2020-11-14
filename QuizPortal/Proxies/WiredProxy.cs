using QuizPortal.Models.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

namespace QuizPortal.Proxies
{
    public class WiredProxy : IWiredProxy
    {
        public async Task<ICollection<ArticleDto>> GetLastFiveArticlesAsync()
        {
            string url = "https://www.wired.com/feed/rss";

            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            List<ArticleDto> articleList = new List<ArticleDto>();

            var fiveFeed = feed.Items.Take(5);

            foreach (SyndicationItem item in fiveFeed)
            {
                var articleDto = new ArticleDto();

                articleDto.ArticleId = item.Id;
                articleDto.Title = item.Title.Text;
                articleDto.Description = item.Summary.Text;

                articleList.Add(articleDto);
            }

            return await Task.FromResult(articleList);
        }
    }
}

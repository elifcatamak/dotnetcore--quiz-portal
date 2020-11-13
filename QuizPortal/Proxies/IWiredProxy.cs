using QuizPortal.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizPortal.Proxies
{
    public interface IWiredProxy
    {
        Task<ICollection<ArticleDto>> GetLastFiveArticlesAsync();
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace HowWeSpendOurMoney.Infrastructure
{
    public interface ITagsRepository
    {
        Task<IEnumerable<string>> GetAll();
        Task AddTag(string tag);
    }
}

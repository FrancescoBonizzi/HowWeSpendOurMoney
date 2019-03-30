using HowWeSpendOurMoney.Domain;
using HowWeSpendOurMoney.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HowWeSpendOurMoney
{
    public class InMemoryTagsRepository : ITagsRepository
    {
        private readonly ICollection<string> _tags;

        public InMemoryTagsRepository()
        {
            _tags = new List<string>()
            {
                "Cibo", "Spesa", "Benzina", "Macchina", "Extra", "Da rivedere", Definitions.IgnoreTag
            };
        }

        public Task AddTag(string tag)
        {
            _tags.Add(tag);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<string>> GetAll()
            => Task.FromResult(_tags.AsEnumerable());
    }
}

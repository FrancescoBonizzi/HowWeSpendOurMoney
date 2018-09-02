using HowWeSpendOurMoney.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HowWeSpendOurMoney.Infrastructure
{
    public interface IImportingRules
    {
        Task AddRule(IRule rule);
        Task<IEnumerable<IRule>> GetAll();
        void ApplyRules(MoneyTransaction transaction);
    }
}

using HowWeSpendOurMoney.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HowWeSpendOurMoney.Infrastructure
{
    public interface IAnalysysRepository
    {
        Task<IEnumerable<PeriodAnalysis>> GetAll();
        Task Store(IEnumerable<PeriodAnalysis> analyses);
    }
}

using HowWeSpendOurMoney.Domain;
using HowWeSpendOurMoney.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HowWeSpendOurMoney
{
    public class InMemoryAnalysysRepository : IAnalysysRepository
    {
        private IEnumerable<PeriodAnalysis> _periodAnalyses;

        public Task<IEnumerable<PeriodAnalysis>> GetAll()
            => Task.FromResult(_periodAnalyses);

        public Task Store(IEnumerable<PeriodAnalysis> analyses)
        {
            _periodAnalyses = analyses;
            return Task.CompletedTask;
        }
    }
}

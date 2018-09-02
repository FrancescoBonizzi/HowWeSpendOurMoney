using HowWeSpendOurMoney.Domain;
using System.Collections.Generic;

namespace HowWeSpendOurMoney.Infrastructure
{
    public interface IRule
    {
        string Description { get; }
        IEnumerable<string> GetTagsToApply(MoneyTransaction transaction);
    }
}

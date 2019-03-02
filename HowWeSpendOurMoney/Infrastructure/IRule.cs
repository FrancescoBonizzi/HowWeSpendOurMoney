using HowWeSpendOurMoney.Domain;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HowWeSpendOurMoney.Infrastructure
{
    /// <summary>
    /// Abstraction of a rule. It should implement also IEqualityComparer to retreive duplicates
    /// </summary>
    public interface IRule : IEqualityComparer<IRule>
    {
        string RuleTypeName { get; }

        [JsonIgnore]
        string Description { get; }
        IEnumerable<string> GetTagsToApply(MoneyTransaction transaction);
    }
}

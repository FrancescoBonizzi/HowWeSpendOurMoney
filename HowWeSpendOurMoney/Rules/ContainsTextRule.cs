using HowWeSpendOurMoney.Domain;
using HowWeSpendOurMoney.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HowWeSpendOurMoney.Rules
{
    public class ContainsTextRule : IRule
    {
        public string TextToSearch { get; set; }
        private readonly IEnumerable<string> _tagsToApply;

        public string Description { get; }

        public ContainsTextRule(
            string textToSearch,
            params string[] tagsToApply)
        {
            if (string.IsNullOrWhiteSpace(textToSearch))
                throw new ArgumentNullException(nameof(textToSearch));

            if (tagsToApply == null || !tagsToApply.Any())
                throw new ArgumentNullException(nameof(tagsToApply));

            TextToSearch = textToSearch;
            _tagsToApply = tagsToApply;

            Description = $"If the transaction contains \"{textToSearch}\", apply tags: \"{string.Join(", ", tagsToApply)}\"";
        }

        public IEnumerable<string> GetTagsToApply(MoneyTransaction transaction)
        {
           
            if (transaction.Description.ToLower().Contains(TextToSearch.ToLower()))
                return _tagsToApply;

            return new List<string>();
        }
    }
}

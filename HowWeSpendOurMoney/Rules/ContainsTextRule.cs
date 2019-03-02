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
        public IEnumerable<string> TagsToApply;

        public string RuleTypeName => nameof(ContainsTextRule);
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
            TagsToApply = tagsToApply;

            Description = $"If the transaction contains \"{textToSearch}\", apply tags: \"{string.Join(", ", tagsToApply)}\"";
        }

        public IEnumerable<string> GetTagsToApply(MoneyTransaction transaction)
        {
            if (transaction.Description.ToLower().Contains(TextToSearch.ToLower()))
                return TagsToApply;

            return new List<string>();
        }

        public bool Equals(IRule x, IRule y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(IRule obj)
        {
            return obj.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var rule = obj as ContainsTextRule;
            return rule != null &&
                   TextToSearch == rule.TextToSearch &&
                   RuleTypeName == rule.RuleTypeName &&
                   Description == rule.Description;
        }

        public override int GetHashCode()
        {
            var hashCode = 155706988;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TextToSearch);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(RuleTypeName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            return hashCode;
        }
    }
}

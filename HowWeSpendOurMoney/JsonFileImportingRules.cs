using HowWeSpendOurMoney.Domain;
using HowWeSpendOurMoney.Infrastructure;
using HowWeSpendOurMoney.Rules;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HowWeSpendOurMoney
{
    public class JsonFileImportingRules : IImportingRules
    {
        private readonly string _filePath;
        private readonly HashSet<IRule> _rules;

        public JsonFileImportingRules(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath));

            _filePath = filePath;

            if (File.Exists(_filePath))
            {
                var rulesRaw = File.ReadAllText(_filePath);
                dynamic rulesInterface = JArray.Parse(rulesRaw);
                _rules = new HashSet<IRule>();
                foreach (var r in rulesInterface)
                {
                    switch (r.RuleTypeName.Value.ToString())
                    {
                        case nameof(ContainsTextRule):

                            var tagsToApplyString = new List<string>();
                            foreach(var c in r.TagsToApply.Children())
                            {
                                tagsToApplyString.Add(c.ToString());
                            }

                            _rules.Add(new ContainsTextRule(
                                r.TextToSearch.Value.ToString(),
                                tagsToApplyString.ToArray()));
                            break;

                        // TODO Other cases

                        default:
                            throw new NotImplementedException($"Rule type: {r.RuleTypeName} not implementated for Json deserialization");
                    }
                }
            }
            else
            {
                _rules = new HashSet<IRule>();
            }

        }

        public Task AddRule(IRule rule)
        {
            if (_rules.Contains(rule))
                return Task.CompletedTask;

            _rules.Add(rule);
            File.WriteAllText(_filePath, JsonConvert.SerializeObject(_rules, Formatting.Indented));
            return Task.CompletedTask;
        }

        public void ApplyRules(MoneyTransaction transaction)
        {
            IEnumerable<string> ruleAppliedTags = transaction.Tags;
            foreach (var rule in _rules)
            {
                ruleAppliedTags = ruleAppliedTags.Concat(rule.GetTagsToApply(transaction));
            }

            transaction.Tags = new ObservableCollection<string>(ruleAppliedTags.Distinct());
        }

        public Task<IEnumerable<IRule>> GetAll()
            => Task.FromResult(_rules.AsEnumerable());
    }
}

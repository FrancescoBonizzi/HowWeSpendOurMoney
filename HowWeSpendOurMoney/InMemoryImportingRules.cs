using HowWeSpendOurMoney.Domain;
using HowWeSpendOurMoney.Exceptions;
using HowWeSpendOurMoney.Infrastructure;
using HowWeSpendOurMoney.Rules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace HowWeSpendOurMoney
{
    public class InMemoryImportingRules : IImportingRules
    {
        private readonly IMoneyTransactionsRepository _moneyTransactionsRepository;
        private readonly ICollection<IRule> _rules;

        public InMemoryImportingRules(IMoneyTransactionsRepository moneyTransactionsRepository)
        {
            _moneyTransactionsRepository = moneyTransactionsRepository ?? throw new ArgumentNullException(nameof(moneyTransactionsRepository));
            _rules = new List<IRule>()
            {
                new ContainsTextRule("superstore", "Cibo", "Spesa"),
                new ContainsTextRule("supermercato", "Cibo", "Spesa"),
                new ContainsTextRule("telecom", "Casa", "Internet"),
                new ContainsTextRule("orangecom", "Cellulare"),
                new ContainsTextRule("restaurant", "Cibo", "Extra", "Uscite"),
                new ContainsTextRule("ristorante", "Cibo", "Extra", "Uscite"),
                new ContainsTextRule("buca dei diavoli", "Cibo", "Extra", "Uscite"),
                new ContainsTextRule("mc donald's", "Cibo", "Extra", "Uscite"),
                new ContainsTextRule("ikea", "Casa"),
                new ContainsTextRule("f24", "Casa", "Bollette"),
                new ContainsTextRule("iren", "Casa", "Corrente elettrica", "Bollette"),
                new ContainsTextRule("nonna silvana", "Casa", "Mobili"),
                new ContainsTextRule("migliore lorena", "Regali", "Extra"),
                new ContainsTextRule("contochiarafrancesco", "Casa"),
                new ContainsTextRule("genialloyd", "Macchina"),
                new ContainsTextRule("favore canta", "Extra"),
                new ContainsTextRule("prague", "Extra", "Uscite"),
                new ContainsTextRule("decathlon", "Extra"),
                new ContainsTextRule("cagna & benelli", "Cibo", "Spesa"),
                new ContainsTextRule("farmacia", "Salute", "Farmacia"),
                new ContainsTextRule("xijun", "Extra", "Casa"),
                new ContainsTextRule("fu hao", "Extra", "Casa"),
                new ContainsTextRule("conad", "Cibo", "Spesa"),
                new ContainsTextRule("montaditos", "Cibo", "Extra", "Uscite"),
                new ContainsTextRule("i monelli", "Cibo", "Extra", "Uscite"),
                new ContainsTextRule("birra del borgo", "Cibo", "Extra", "Uscite"),
                new ContainsTextRule("asian mood", "Cibo", "Extra", "Uscite"),
                new ContainsTextRule("il torchio", "Cibo", "Extra", "Uscite"),
                new ContainsTextRule("sabah srl", "Cibo", "Extra", "Uscite"),
                new ContainsTextRule("time out", "Cibo", "Extra", "Uscite"),
                new ContainsTextRule("tenji", "Cibo", "Extra", "Uscite"),
                new ContainsTextRule("sushiko", "Cibo", "Extra", "Uscite"),
                new ContainsTextRule("liang sas", "Cibo", "Extra", "Uscite"),
                new ContainsTextRule("dama capricciosa", "Cibo", "Extra", "Uscite"),
                new ContainsTextRule("my sushi", "Cibo", "Extra", "Uscite"),
                new ContainsTextRule("globo calzature", "Vestiti", "Extra"),
                new ContainsTextRule("ovs", "Vestiti", "Extra"),
                new ContainsTextRule("well and fit", "Extra", "Palestra", "Salute"),
                new ContainsTextRule("mollard", "Regali", "Extra"),
                new ContainsTextRule("centro toys", "Regali", "Extra"),
                new ContainsTextRule("liberalia", "Libreria", "Extra"),
                new ContainsTextRule("totalerg", "Benzina", "Macchina"),
                new ContainsTextRule("l v noceto ita", "Benzina", "Macchina"),
                new ContainsTextRule("q8", "Benzina", "Macchina"),
                new ContainsTextRule("ads san giacomo nord", "Benzina", "Macchina"),
                new ContainsTextRule("pizzoni", "Extra", "Regali"),
                new ContainsTextRule("bata factory", "Extra", "Vestiti"),
                new ContainsTextRule("food", "Cibo", "Extra", "Uscite"),
                new ContainsTextRule("burger", "Cibo", "Extra", "Uscite"),
                new ContainsTextRule("pizzeria", "Cibo", "Extra", "Uscite"),
                new ContainsTextRule("grease", "Cibo", "Extra", "Uscite"),
                new ContainsTextRule("the space cinema", "Extra", "Uscite"),
                new ContainsTextRule("commissioni", "Banca", "Commissioni"),
                new ContainsTextRule("comm.", "Banca", "Commissioni"),
                new ContainsTextRule("int. e comp.", "Banca", "Commissioni"),
                new ContainsTextRule("bpmvita", "Casa", "Mutuo"),
                new ContainsTextRule("ricarica", "Extra"),
                new ContainsTextRule("fiera milano spa", "Extra"),
                new ContainsTextRule("centrale m2 milano", "Extra"),
                new ContainsTextRule("comune di collecchio u colle", "Extra"),
                new ContainsTextRule("noceto noceto ita", "Extra"),
                new ContainsTextRule("m.av./r.av.", "Extra"),
                new ContainsTextRule("fidenzatram", "Cibo", "Extra", "Uscite"),
                new ContainsTextRule("lidl", "Cibo", "Spesa"),
                new ContainsTextRule("traverso", "Casa", "Amministratore condominio"),
                new ContainsTextRule("cartimpronta", "CDC", "Extra", "Uscite"),
                new ContainsTextRule("prel", "Contanti"),
                new ContainsTextRule("atm", "Contanti"),
                new ContainsTextRule("bed and breakfast", "Extra"),
                new ContainsTextRule("bed & breakfast", "Extra"),
                new ContainsTextRule("distributore", "Benzina", "Macchina"),
                new ContainsTextRule("pv3038", "Benzina", "Macchina"),
                new ContainsTextRule("sulis monica", "Benzina", "Macchina"),
                new ContainsTextRule("shell", "Benzina", "Macchina"),
                new ContainsTextRule("eni rete", "Benzina", "Macchina"),
                new ContainsTextRule("europam", "Benzina", "Macchina"),
                new ContainsTextRule("riccardi lino sergio", "Benzina", "Macchina"),
                new ContainsTextRule("ieva vincenzo", "Benzina", "Macchina"),
                new ContainsTextRule("mutuo", "Casa", "Mutuo"),
                new ContainsTextRule("pronto carni", "Cibo", "Spesa"),
                new ContainsTextRule("paladini", "Cibo", "Spesa"),
                //neww ContainsTextRule("vostra disposizione", "Bonifici"),
                new ContainsTextRule("canone conto corr", "Banca"),
                new ContainsTextRule("bar", "Extra", "Uscite", "Cibo"),
                new ContainsTextRule("com.ord.ricorrenti", "Commissioni", "Banca"),
                new ContainsTextRule("francesco bonizzi-chiara bertoletti", "Casa"),
                new ContainsTextRule("bollettino", "Extra"),
                new ContainsTextRule("blu ranieri", "Cibo", "Spesa"),
                new ContainsTextRule("aldi", "Cibo", "Spesa"),
            };
            // TODO farlo caricare da file json anche
            // manca alla fine metto tutto in un file json, anche la parte sql, mah
        }

        public Task AddRule(IRule rule)
        {
            if (_rules.Contains(rule))
                throw new DuplicatedRuleException();

            _rules.Add(rule);
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

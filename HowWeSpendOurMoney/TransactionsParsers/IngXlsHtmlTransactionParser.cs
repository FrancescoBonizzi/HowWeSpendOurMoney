using HowWeSpendOurMoney.Domain;
using HowWeSpendOurMoney.Exceptions;
using HowWeSpendOurMoney.Infrastructure;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace HowWeSpendOurMoney.TransactionsParsers
{
    // Ing = Ing direct
    public class IngXlsHtmlTransactionParser : IMoneyTransactionsParser
    {
        public IEnumerable<MoneyTransaction> ParseTransactions(IEnumerable<string> moneyTransactionsRaw)
        {
            if (moneyTransactionsRaw?.Any() != true)
                throw new MoneyTransactionsParsingException("moneyTransactionsRaw cannot be null or empty");

            var allContent = string.Concat(moneyTransactionsRaw);
            var moneyTransactions = new List<MoneyTransaction>();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(allContent);
            _extractTable(doc.DocumentNode, moneyTransactions);

            return moneyTransactions;
        }

        private static void _extractTable(HtmlNode documentNode, List<MoneyTransaction> moneyTransactions)
        {
            if (documentNode.NodeType == HtmlNodeType.Element && documentNode.Name == "table")
            {
                for (int trIndex = 2; trIndex < documentNode.ChildNodes.Count; trIndex++)
                {
                    HtmlNode[] td = documentNode
                        .ChildNodes[trIndex]
                        .ChildNodes
                        .Where(node => node.NodeType == HtmlNodeType.Element)
                        .ToArray();

                    if (td.Length == 5)
                    {
                        moneyTransactions.Add(MoneyTransactionBuilder.Create()
                            .WithDescription(td[3].InnerText)
                            .InThisDates(td[0].InnerText, td[1].InnerText, "dd/MM/yyyy")
                            .WithMovementType(td[2].InnerText)
                            .WithThisTotal(_clearDecimalValue(td[4].InnerText), "it-IT")
                            .WithThisCurrency("EURO")
                            .Build());
                    }
                }
            }

            if (documentNode.HasChildNodes)
            {
                foreach (HtmlNode node in documentNode.ChildNodes)
                {
                    _extractTable(node, moneyTransactions);
                }
            }
        }

        private static string _clearDecimalValue(string innerText)
        {
            return innerText.Replace("&euro;", " ").Trim();
        }
    }
}

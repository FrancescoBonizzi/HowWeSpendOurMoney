# HowWeSpendOurMoney
A simple library to parse money transactions from bank account and see some statistics.

# The idea
The main idea is to parse a list of `MoneyTransaction`(s) that come from the typical export button of a bank account backend to **categorize** them. In this abstraction each category is called `Tag` and will be used for further analysys such as answering to some questions:
- How many money I spent last year going to the pub?
- How much does my car cost per month?
- ...

IMHO those questions are useful to organize and take decisions about everyday life, so I decided to code an application to help me out.

# The abstraction
You can see much of the whole application logic by reading `MoneyTransactionsImporter` that contains and consumes all abstractions I defined:
1. You parse a list of raw transactions (strings)
2. With a frontend and some rules you assign tags to each transaction
3. You store the raw list and the parsed list somewhere for history purposes
4. You analyze each transaction within a specific date range (Monthly, Yearly)
5. You store the analysys
6. You represent graphically the analysys

In the first implementations, everything is *InMemory*, but in a more mature implementations the storage part could be a database, a file, and so on.

# Building
Simply clone this repository and build the `HowWeSpendOurMoney.sln` solution.

# How to contribute
- Report any issues
- Implementing some other new `IMoneyTransactionsParser.cs` for other banks account output
- Add some metrics to `PeriodAnalysis` to give a richer result
- Create a new frontend such as a web page, or another desktop application, or improve the existing one
- Implement some new `IRule`: the first one I implemenented (`ContainsTextRule`) is very basic!
- Just telling your opinion :-)

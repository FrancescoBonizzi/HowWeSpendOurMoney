using System;

namespace HowWeSpendOurMoney.Factory
{
    public static class DateTimeOffsetFactory
    {
        public static DateTimeOffset CreateAtFirstDayOfTheMonth(int month, int year)
            => new DateTimeOffset(year, month, 1, 0, 0, 0, DateTimeOffset.Now.Offset);

        public static DateTimeOffset CreateAtFirstDayOfTheYear(int year)
            => CreateAtFirstDayOfTheMonth(1, year);
    }
}

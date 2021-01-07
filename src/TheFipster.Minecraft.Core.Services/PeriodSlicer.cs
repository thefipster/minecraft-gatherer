using System;
using System.Collections.Generic;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Core.Domain;

namespace TheFipster.Minecraft.Core.Services
{
    public class PeriodSlicer : IPeriodSlicer
    {
        public IEnumerable<TimePeriod> Slice(Periods period)
        {
            var periods = new List<TimePeriod>();

            switch (period)
            {
                case Periods.Daily:
                    {
                        var pointer = DateTime.UtcNow.Date.AddDays(-30);
                        while (pointer <= DateTime.UtcNow.Date)
                        {
                            var label = pointer.ToString("dd.MM");
                            periods.Add(new TimePeriod(pointer, pointer.AddDays(1), label));
                            pointer = pointer.AddDays(1);
                        }
                        break;
                    }
                case Periods.Weekly:
                    {
                        var pointer = DateTime.UtcNow.Date.AddDays(-1 * 7 * 26);
                        pointer = getLastMondayFrom(pointer);
                        while (pointer <= DateTime.UtcNow.Date)
                        {
                            var label = pointer.ToString("dd.MM");
                            periods.Add(new TimePeriod(pointer, pointer.AddDays(7), label));
                            pointer = pointer.AddDays(7);
                        }
                        break;
                    }
                case Periods.Monthly:
                    {
                        var pointer = new DateTime(DateTime.UtcNow.Year - 1, DateTime.UtcNow.Month, 1);
                        while (pointer <= DateTime.UtcNow.Date)
                        {
                            var label = pointer.ToString("MMM y");
                            periods.Add(new TimePeriod(pointer, pointer.AddMonths(1), label));
                            pointer = pointer.AddMonths(1);
                        }
                        break;
                    }
            }

            return periods;
        }

        private DateTime getLastMondayFrom(DateTime referenceDate)
        {
            while (referenceDate.DayOfWeek != DayOfWeek.Monday)
                referenceDate = referenceDate.AddDays(-1);

            return referenceDate;
        }
    }
}

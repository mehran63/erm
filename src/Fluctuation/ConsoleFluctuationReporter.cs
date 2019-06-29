using System;
using System.Collections.Generic;
using System.Text;
using Fluctuation.Models;

namespace Fluctuation
{
    public class ConsoleFluctuationReporter : IFluctuationReporter
    {
        public void ReportRecord(string fileName, decimal median, FileRecord record)
        {
            //ConsoleWriter could be defined and injected to make it testable
            Console.WriteLine($"{fileName} {record.DateTime} {record.Value} {median}");
        }
    }
}

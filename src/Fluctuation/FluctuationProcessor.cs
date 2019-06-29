using Fluctuation.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;

namespace Fluctuation
{
    public class FluctuationProcessor : IFluctuationProcessor
    {
        private readonly IMedianCalculator medianCalculator;
        private readonly IFileRecordLoader fileLoader;
        private readonly IFluctuationReporter fluctuationReporter;

        public FluctuationProcessor(
            IMedianCalculator medianCalculator,
            IFileRecordLoader fileLoader,
            IFluctuationReporter fluctuationReporter)
        {
            this.medianCalculator = medianCalculator;
            this.fileLoader = fileLoader;
            this.fluctuationReporter = fluctuationReporter;
        }

        public void Process(IFileInfo file, decimal differencePercentage)
        {
            var records = fileLoader.Load(file);
            if (records.Count > 0)
            {
                var sortedRecords = records.OrderBy(r => r.Value).ToList();

                var median = medianCalculator.Calculate(
                    sortedRecords.Select(r => r.Value).ToList());

                var trigger = median * (differencePercentage / 100);

                foreach (var record in sortedRecords)
                {
                    var diff = Math.Abs(record.Value - median);
                    if (diff > trigger)
                    {
                        fluctuationReporter.ReportRecord(file.Name, median, record);
                    }
                }
            }
        }
    }
}

using Fluctuation.Models;

namespace Fluctuation
{
    public interface IFluctuationReporter
    {
        void ReportRecord(string fileName, decimal median, FileRecord record);
    }
}
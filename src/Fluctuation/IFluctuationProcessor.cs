using System.IO.Abstractions;

namespace Fluctuation
{
    public interface IFluctuationProcessor
    {
        void Process(IFileInfo file, decimal differencePercentage);
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Fluctuation.Models
{
    public class FluctuationProcessingSettings
    {
        public string InputFilesDirectory { get; set; }
        public string InputFileSearchPattern { get; set; }
        public decimal DifferencePercentage { get; set; }
    }
}

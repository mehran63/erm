using Fluctuation.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Text;

namespace Fluctuation
{
    public class BatchFluctuationProcessor
    {
        private readonly IFluctuationProcessor fluctuationProcessor;
        private readonly FluctuationProcessingSettings settings;
        private readonly IFileSystem fileSystem;

        public BatchFluctuationProcessor(
            IFluctuationProcessor fluctuationProcessor,
            IOptions<FluctuationProcessingSettings> settings,
            IFileSystem fileSystem)
        {
            this.fluctuationProcessor = fluctuationProcessor;
            this.settings = settings.Value;
            this.fileSystem = fileSystem;
        }

        public void Process()
        {
            if (settings.DifferencePercentage < 0)
                settings.DifferencePercentage = Math.Abs(settings.DifferencePercentage);

            var fileNames = fileSystem.Directory.GetFiles(
                settings.InputFilesDirectory,
                settings.InputFileSearchPattern);

            foreach (var fileName in fileNames)
            {
                var fileInfo = fileSystem.FileInfo.FromFileName(fileName);
                fluctuationProcessor.Process(fileInfo, settings.DifferencePercentage);
            }
        }
    }
}

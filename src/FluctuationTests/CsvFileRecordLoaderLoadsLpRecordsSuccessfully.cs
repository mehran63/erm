using Fluctuation;
using Fluctuation.Models;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Text;
using Xunit;

namespace FluctuationTests
{
    public class CsvFileRecordLoaderLoadsLpRecordsSuccessfully
    {
        private List<FileRecord> loadedRecord;
        private MockFileSystem fileSystem;

        public CsvFileRecordLoaderLoadsLpRecordsSuccessfully()
        {
            IFileInfo fileInfo = GivenACsvInputFile();
            loadedRecord = WhenLoadsTheFile(fileInfo);
        }

        private IFileInfo GivenACsvInputFile()
        {
            fileSystem = new MockFileSystem();

            var fileData = new MockFileData(
                   @"MeterPoint Code,Serial Number,Plant Code,Date/Time,Data Type,Data Value,Units,Status" + Environment.NewLine +
                   @"210095893,210095893,ED031000001,31/08/2015 00:45:00,Import Wh Total,0.000000,kwh," + Environment.NewLine);

            fileSystem.AddFile("c:/test.csv", fileData);
            return fileSystem.FileInfo.FromFileName("c:/test.csv");
        }

        private List<FileRecord> WhenLoadsTheFile(IFileInfo fileInfo)
        {
            var mockLogger = new Mock<ILogger<CsvFileRecordLoader>>();
            var sut = new CsvFileRecordLoader(mockLogger.Object);
            return sut.Load(fileInfo);
        }

        [Fact]
        public void ThenTheRecordLoadedCorrectly()
        {
            loadedRecord.Should().BeEquivalentTo(new List<FileRecord>
            {
                new FileRecord
                {
                    DateTime = "31/08/2015 00:45:00",
                    Value = 0.000000M
                }
            });
        }

    }
}

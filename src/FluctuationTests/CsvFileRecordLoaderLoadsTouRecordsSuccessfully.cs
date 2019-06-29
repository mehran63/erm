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
    public class CsvFileRecordLoaderLoadsTouRecordsSuccessfully
    {
        private List<FileRecord> loadedRecord;
        private MockFileSystem fileSystem;

        public CsvFileRecordLoaderLoadsTouRecordsSuccessfully()
        {
            IFileInfo fileInfo = GivenACsvInputFile();
            loadedRecord = WhenLoadsTheFile(fileInfo);
        }

        private IFileInfo GivenACsvInputFile()
        {
            fileSystem = new MockFileSystem();

            var fileData = new MockFileData(
                   @"MeterPoint Code,Serial Number,Plant Code,Date/Time,Data Type,Energy,Maximum Demand,Time of Max Demand,Units,Status,Period,DLS Active,Billing Reset Count,Billing Reset Date/Time,Rate" + Environment.NewLine +
                   @"212621147,212621147,ED011300247,11/09/2015 00:41:07,Export Wh Total,378331.600000,1118.448000,30/12/1899 00:00:00,kwh,.....R....,Total,False,26,01/09/2015 00:00:00,Unified" + Environment.NewLine);

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
                    DateTime = "11/09/2015 00:41:07",
                    Value = 378331.600000M
                }
            });
        }

    }
}

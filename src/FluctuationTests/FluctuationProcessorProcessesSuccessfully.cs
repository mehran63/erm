using Fluctuation;
using Fluctuation.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Text;
using Xunit;

namespace FluctuationTests
{
    public class FluctuationProcessorProcessesSuccessfully
    {
        private Mock<IFluctuationReporter> mockFluctuationReporter;
        private Mock<IFileInfo> mockFileInfo;
        private string testFileName = "testFile";
        private decimal differencePercentage = 20M;
        private FileRecord fileRecord1 = new FileRecord() { DateTime = "dateTime1", Value = 3.2M };
        private FileRecord fileRecord2 = new FileRecord() { DateTime = "dateTime2", Value = 10.2M };
        private FileRecord fileRecord3 = new FileRecord() { DateTime = "dateTime3", Value = 11.8M };
        public FluctuationProcessorProcessesSuccessfully()
        {
            GivenAInpuFile();
            WhenProcess();
        }

        private void GivenAInpuFile()
        {
            mockFileInfo = new Mock<IFileInfo>();
            mockFileInfo.Setup(m => m.Name).Returns(testFileName);
        }

        private void WhenProcess()
        {
            mockFluctuationReporter = new Mock<IFluctuationReporter>();
            var mockFileRecordLoader = new Mock<IFileRecordLoader>();
            var mockMedianCalculator = new Mock<IMedianCalculator>();
            var sut = new FluctuationProcessor(
                mockMedianCalculator.Object,
                mockFileRecordLoader.Object,
                mockFluctuationReporter.Object
                );

            mockFileRecordLoader
                .Setup(m => m.Load(mockFileInfo.Object))
                .Returns(new List<FileRecord>()
                {
                    fileRecord1,
                    fileRecord2,
                    fileRecord3
                });

            mockMedianCalculator
                .Setup(m => m.Calculate(It.IsAny<List<decimal>>()))
                .Returns(10.2M);

            sut.Process(mockFileInfo.Object, differencePercentage);
        }

        [Fact]
        public void ThenReportsCorrectly()
        {
            mockFluctuationReporter
               .Verify(m => m.ReportRecord(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<FileRecord>()),
               Times.Once);

            mockFluctuationReporter
                .Verify(m => m.ReportRecord(testFileName, 10.2M, fileRecord1));
        }

    }
}

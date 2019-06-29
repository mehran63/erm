using Fluctuation;
using Fluctuation.Models;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Text;
using Xunit;

namespace FluctuationTests
{
    public class FluctuationProcessorIteratesFilesSuccessfully
    {
        private readonly decimal differencePercentage = 20M;
        private Mock<IFluctuationProcessor> fluctuationProcessorMock;
        private string file1 = "LP_210095893_20150901T011608049.csv";
        private string file2 = "TOU_212621145_20150911T022358.csv";

        public FluctuationProcessorIteratesFilesSuccessfully()
        {
            var batchFluctuationProcessor = GivenADirectoyOfTwoInputFiles();
            WhenProcessed(batchFluctuationProcessor);
        }

        private BatchFluctuationProcessor GivenADirectoyOfTwoInputFiles()
        {
            fluctuationProcessorMock = new Mock<IFluctuationProcessor>();

            var mockSetting = new Mock<IOptions<FluctuationProcessingSettings>>();
            mockSetting
                .Setup(m => m.Value)
                .Returns(new FluctuationProcessingSettings
                {
                    DifferencePercentage = differencePercentage,
                    InputFilesDirectory = "C:/inputDirectory/",
                    InputFileSearchPattern = "*.csv"
                });

            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile($"C:/inputDirectory/{file1}", new MockFileData("doesn't matter yyy"));
            mockFileSystem.AddFile($"C:/inputDirectory/{file2}", new MockFileData("doesn't matter xxx"));

            var sut = new BatchFluctuationProcessor(
                fluctuationProcessorMock.Object,
                mockSetting.Object,
                mockFileSystem);

            return sut;
        }

        private void WhenProcessed(BatchFluctuationProcessor batchFluctuationProcessor)
        {
            batchFluctuationProcessor.Process();
        }

        [Fact]
        public void AllBothFilesAreIterated()
        {
            fluctuationProcessorMock.Verify(
                m => m.Process(It.IsAny<IFileInfo>(), It.IsAny<decimal>()), Times.Exactly(2));
            fluctuationProcessorMock.Verify(
                m => m.Process(It.Is<IFileInfo>(f => f.Name == file1), differencePercentage));
            fluctuationProcessorMock.Verify(
                m => m.Process(It.Is<IFileInfo>(f => f.Name == file2), differencePercentage));
        }
    }
}

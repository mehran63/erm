using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using CsvHelper;
using Fluctuation.CsvMappings;
using Fluctuation.Models;
using Microsoft.Extensions.Logging;

namespace Fluctuation
{
    public class CsvFileRecordLoader : IFileRecordLoader
    {
        private readonly ILogger logger;

        public CsvFileRecordLoader(ILogger<CsvFileRecordLoader> logger)
        {
            this.logger = logger;
        }

        public List<FileRecord> Load(IFileInfo file)
        {
            using (var stream = file.OpenRead())
            using (var reader = new StreamReader(stream))
            using (var csvReader = new CsvReader(reader))
            {
                csvReader.Configuration.RegisterClassMap<FileRecordMap>();
                csvReader.Configuration.ReadingExceptionOccurred = (ex) =>
                {
                    logger.LogError(ex, $"Row record:{csvReader.Context.RawRecord}");
                    return false;
                };
                return csvReader.GetRecords<FileRecord>().ToList();
            }
        }
    }
}

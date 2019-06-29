using CsvHelper.Configuration;
using Fluctuation.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fluctuation.CsvMappings
{
    public class FileRecordMap : ClassMap<FileRecord>
    {
        public FileRecordMap()
        {
            Map(m => m.DateTime).Index(3);
            Map(m => m.Value).Index(5);
        }
    }
}

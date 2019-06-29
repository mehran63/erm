using Fluctuation.Models;
using System.Collections.Generic;
using System.IO.Abstractions;

namespace Fluctuation
{
    public interface IFileRecordLoader
    {
        List<FileRecord> Load(IFileInfo file);
    }
}
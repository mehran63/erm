using System;
using System.Collections.Generic;
using System.Text;

namespace Fluctuation.Models
{
    //instead of having a generic class which represent both types of record (LP and TOU) 
    //The other possible approach is having two separate implementations with a base interface or abstract class.
    public class FileRecord
    {
        public string DateTime { get; set; }

        public decimal Value { get; set; }
    }
}
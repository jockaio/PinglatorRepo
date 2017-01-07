using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace Cantofy3.Helpers
{
    public static class SequenceHelper
    {
        public static int GetSequenceNumber()
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + @"WordSearch.txt";
            
            if (File.Exists(fileName))
            {
                int seqNumber = Int32.Parse(System.IO.File.ReadAllText(fileName)) + 1;
                System.IO.File.WriteAllText(fileName, seqNumber.ToString());
                return seqNumber;
            }
            else
            {
                System.IO.File.WriteAllText(fileName, "1");
                return 1;
            }
        }
        
    }
}
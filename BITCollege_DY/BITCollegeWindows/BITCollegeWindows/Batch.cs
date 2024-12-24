using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace BITCollegeWindows
{
    /// <summary>
    /// Batch:  This class provides functionality that will validate
    /// and process incoming xml files.
    /// </summary>
    public class Batch
    {

        private String inputFileName;
        private String logFileName;
        private String logData;


        private void ProcessErrors(IEnumerable<XElement> beforeQuery, IEnumerable<XElement> afterQuery, String message)
        {

        }

        private void ProcessHeader()
        {

        }

        private void ProcessDetails()
        {

        }

        private void ProcessTransactions(IEnumerable<XElement> transactionRecords)
        {

        }

        public String WriteLogData()
        {
            return logData;
        }

        public void ProcessTransmission(String programAcronym)
        {

        }
    }
}

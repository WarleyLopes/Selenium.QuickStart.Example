using NUnit.Framework;
using System.Collections;
using Selenium.QuickStart.Utilities;
using System.Resources;

namespace Base2.Mantis.Test.Automation.Challenge.Data
{
    public class TestData
    {
        public static IEnumerable IssuesToReport
        {
            get
            {
                foreach(string[] data in CsvReader.GetData(Properties.Resources.MyDataSource))
                    yield return new TestCaseData(data);
            }
        }
    }
}

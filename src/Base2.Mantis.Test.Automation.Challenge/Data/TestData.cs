using NUnit.Framework;
using System.Collections;
using Selenium.QuickStart.Utilities;

namespace Base2.Mantis.Test.Automation.Challenge.Data
{
    public class TestData
    {
        public static IEnumerable IssuesToReport
        {
            get
            {
                foreach(string[] data in CsvReader.GetData("MyDataSource"))
                    yield return new TestCaseData(data);
            }
        }
    }
}

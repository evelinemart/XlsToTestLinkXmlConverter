using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLinkXmlConverter
{
    class ExelDataReader
    {
        public List<TestCaseModel> ReadExelFile(string filePath)
        {
            List<TestCaseModel> testCases = new List<TestCaseModel>();
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                int i = 0;
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        testCases.Add(new TestCaseModel());
                        testCases[i].steps = new List<StepModel>();
                        while (reader.Read())
                        {
                            if (i == 0)
                                continue;
                            testCases[i]._name = reader.GetString(0);
                            testCases[i].summary = reader.GetString(1);
                            testCases[i].preconditions = reader.GetString(2);
                            testCases[i].execution_type = int.TryParse(reader.GetString(3), out int ex) ? ex : 1;
                            testCases[i].importance = int.TryParse(reader.GetString(4), out int im) ? im : 3;
                            testCases[i].estimated_exec_duration = int.TryParse(reader.GetString(5), out int es) ? es : 3;
                            testCases[i].status = int.TryParse(reader.GetString(6), out int st) ? st : 1;
                            testCases[i].is_open = 1;
                            testCases[i].active = 1;
                            testCases[i].steps.Add(new StepModel()
                            {
                                step_number = 1,
                                actions = reader.GetString(7),
                                expectedresults = reader.GetString(8),
                                execution_type = int.TryParse(reader.GetString(9), out int stex) ? stex : 1
                            });
                        }
                    } while (reader.NextResult());
                }
            }
        }
    }
}

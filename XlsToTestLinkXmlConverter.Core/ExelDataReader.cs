using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using XlsToTestLinkXmlConverter.Core.Enums;
using XlsToTestLinkXmlConverter.Core.Extensions;

namespace XlsToTestLinkXmlConverter.Core
{
    class ExelDataReader
    {
        internal static List<TestCaseModel> ReadExelFile(string filePath, BackgroundWorker worker)
        {
            List<TestCaseModel> testCases = new List<TestCaseModel>();
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    int i = -1;
                    do
                    {
                        int step_no = 0;
                        while (reader.Read())
                        {
                            string name = (string)reader.GetValue(0);
                            if (name.Equals(nameof(TestCaseModel.name), StringComparison.InvariantCultureIgnoreCase) || (reader.IsDBNull(0) && reader.IsDBNull(1) && reader.IsDBNull(2) && reader.IsDBNull(3) && reader.IsDBNull(4) &&
                                reader.IsDBNull(5) && reader.IsDBNull(6) && reader.IsDBNull(7) && reader.IsDBNull(8)))
                                continue;
                            if (!string.IsNullOrEmpty(name?.Trim()))
                            {
                                step_no = 0;
                                testCases.Add(new TestCaseModel());
                                testCases[++i].steps = new List<StepModel>();
                                testCases[i].name = name;
                                testCases[i].summary = (string)reader.GetValue(1);
                                testCases[i].preconditions = (string)reader.GetValue(2);
                                testCases[i].execution_type = reader.GetValue(3)?.ToEnumInt<ExecutionType>();
                                testCases[i].importance = reader.GetValue(4)?.ToEnumInt<Importance>();
                                testCases[i].estimated_exec_duration = reader.GetValue(5) != null && int.TryParse(reader.GetValue(5).ToString(), out int est) ? est : 15;
                            }
                            testCases[i].steps.Add(new StepModel()
                            {
                                step_number = ++step_no,
                                actions = (string)reader.GetValue(6),
                                expectedresults = (string)reader.GetValue(7),
                                execution_type = reader.GetValue(8)?.ToEnumInt<ExecutionType>()
                            });
                            worker.ReportProgress(i+step_no);
                        }
                    } while (reader.NextResult());
                }
                return testCases;
            }
        }
    }
}

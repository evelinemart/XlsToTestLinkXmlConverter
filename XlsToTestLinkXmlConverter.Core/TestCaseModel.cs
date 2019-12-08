using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XlsToTestLinkXmlConverter.Core.Enums;

namespace XlsToTestLinkXmlConverter.Core
{
    class TestCaseModel
    {
        public string name { get; set; }
        public string summary { get; set; }
        public string preconditions { get; set; }
        public ExecutionType? execution_type { get; set; }
        public Importance? importance { get; set; }
        public int estimated_exec_duration { get; set; } = 15;
        public int status { get; set; } = 1;
        public int is_open { get; set; } = 1;
        public int active { get; set; } = 1;
        public List<StepModel> steps { get; set; }
    }
}

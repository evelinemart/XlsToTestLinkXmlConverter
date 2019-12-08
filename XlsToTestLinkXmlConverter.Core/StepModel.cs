using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XlsToTestLinkXmlConverter.Core.Enums;

namespace XlsToTestLinkXmlConverter.Core
{
    class StepModel
    {
        public int step_number { get; set; }
        public string actions { get; set; }
        public string expectedresults { get; set; }
        public ExecutionType? execution_type { get; set; }
    }
}

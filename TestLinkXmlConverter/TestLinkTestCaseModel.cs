using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLinkXmlConverter
{
    public class TestCaseModel
    {
        public string _name { get; set; }
        public string summary { get; set; }
        public string preconditions { get; set; }
        public int execution_type { get; set; }
        public int importance { get; set; }
        public int estimated_exec_duration { get; set; }
        public int status { get; set; }
        public int is_open { get; set; }
        public int active { get; set; }
        public List<StepModel> steps { get; set; }

    }
}

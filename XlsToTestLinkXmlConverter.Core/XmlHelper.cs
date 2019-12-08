using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace XlsToTestLinkXmlConverter.Core
{
    class XmlHelper
    {
        internal static XDocument CreateTestLinkXml(List<TestCaseModel> testCases, BackgroundWorker worker)
        {
            XDocument xdoc = new XDocument();
            XElement testcases = new XElement("testcases");
            int i = 0;
            foreach(TestCaseModel tc in testCases)
            {
                XElement testcase = new XElement("testcase");
                XAttribute name = new XAttribute(nameof(TestCaseModel.name), tc.name);
                XElement summary = new XElement(nameof(TestCaseModel.summary), tc.summary);
                XElement preconditions = new XElement(nameof(TestCaseModel.preconditions), tc.preconditions);
                XElement execution_type = new XElement(nameof(TestCaseModel.execution_type), (int?)tc.execution_type);
                XElement importance = new XElement(nameof(TestCaseModel.importance), (int?)tc.importance);
                XElement estimated_exec_duration = new XElement(nameof(TestCaseModel.estimated_exec_duration), tc.estimated_exec_duration);
                XElement status = new XElement(nameof(TestCaseModel.status), tc.status);
                XElement is_open = new XElement(nameof(TestCaseModel.is_open), tc.is_open);
                XElement active = new XElement(nameof(TestCaseModel.active), tc.active);
                XElement steps = new XElement(nameof(TestCaseModel.steps));
                foreach(StepModel st in tc.steps)
                {
                    XElement step = new XElement("step");
                    XElement step_number = new XElement(nameof(StepModel.step_number), st.step_number);
                    XElement actions = new XElement(nameof(StepModel.actions), st.actions);
                    XElement expectedresults = new XElement(nameof(StepModel.expectedresults), st.expectedresults);
                    XElement step_execution_type = new XElement(nameof(StepModel.execution_type), (int?)st.execution_type);
                    step.Add(step_number, actions, expectedresults, step_execution_type);
                    steps.Add(step);
                }
                testcase.Add(name, summary, preconditions, execution_type, importance, estimated_exec_duration, status, is_open, active, steps);
                testcases.Add(testcase);
                worker.ReportProgress(++i);
            }
            xdoc.Add(testcases);
            return xdoc;
        }
    }
}

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace XlsToTestLinkXmlConverter.Core
{
    public class XlsToTestLinkXmlConverter
    {
        public string XlsFilePath { get; private set; }
        public string XmlFilePath { get; private set; }

        public string OpenXlsFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                openFileDialog.Filter = "XLS files (*.xls)|*.xls|XLSX files (*.xlsx)|*.xlsx";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    XlsFilePath = openFileDialog.FileName;
                }
            }
            return XlsFilePath;
        }

        public XDocument ProcessFile(BackgroundWorker worker)
        {
            worker.ReportProgress(0);
            try
            {
                List<TestCaseModel> testCases = ExelDataReader.ReadExelFile(XlsFilePath, worker);
                worker.ReportProgress(50);
                XDocument xDocument = XmlHelper.CreateTestLinkXml(testCases, worker);
                worker.ReportProgress(100);
                return xDocument;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Cannot create XML document. Exception: {e.Message}", "Error processing document", MessageBoxButtons.OK);
                return null;
            }
        }

        public string SaveXmlFile(XDocument xDocument)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                saveFileDialog.Filter = "XML files (*.xml)|*.xml";
                if(saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    XmlFilePath = saveFileDialog.FileName;
                }
            }
            try
            {
                xDocument.Save(XmlFilePath);
            } 
            catch(Exception e)
            {
                MessageBox.Show($"Cannot save XML document. Exception: {e.Message}", "Error saving document", MessageBoxButtons.OK);
            }
            return XmlFilePath;
        }
    }
}

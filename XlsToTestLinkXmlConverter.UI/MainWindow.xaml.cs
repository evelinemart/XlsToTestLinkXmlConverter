using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace XlsToTestLinkXmlConverter.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string PROCESSING = "Processing file...",
            FINISHED_PROCESSING = "File processed successfully!",
            ERROR_PROCESSING = "Error processing file",
            FINISHED_SAVING = "File saved successfully!";
        private readonly Core.XlsToTestLinkXmlConverter converter;
        private readonly BackgroundWorker worker;
        private XDocument xDocument;

        public MainWindow()
        {
            InitializeComponent();
            converter = new Core.XlsToTestLinkXmlConverter();
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.WorkerReportsProgress = true;
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressFile.Value = e.ProgressPercentage;
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            xDocument = (XDocument)e.Result;
            LblProgress.Content = xDocument != null ? FINISHED_PROCESSING : ERROR_PROCESSING;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = converter.ProcessFile(worker);
        }

        private void BtnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            LblSaveProgress.Content = string.Empty;
            TxtXlsFilePath.Text = converter.OpenXlsFile();
            LblProgress.Content = PROCESSING;
            worker.RunWorkerAsync();
        }

        private void MenuConvert_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuExportExample_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuSetBasicMapping_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuSetCustomMapping_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuSetKeywordsMapping_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuHelp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            if(xDocument != null)
                TxtXmlFilePath.Text = converter.SaveXmlFile(xDocument);
            LblSaveProgress.Content = FINISHED_SAVING;
        }

        private void StackPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ProgressFile.Width = MainPanel.ActualWidth - LblProgress.ActualWidth - MainPanel.Margin.Left * 2;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

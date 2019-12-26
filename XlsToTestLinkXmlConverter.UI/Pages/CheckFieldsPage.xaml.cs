using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XlsToTestLinkXmlConverter.UI.Pages
{
    /// <summary>
    /// Interaction logic for CheckBasicFieldsPage.xaml
    /// </summary>
    public partial class CheckFieldsPage : Page
    {
        private const string HEADER_TEMPLATE = "Check {0} fields mapping";

        public CheckFieldsPage(string fieldType)
        {
            InitializeComponent();
            LabelHeader.Content = string.Format(HEADER_TEMPLATE, fieldType);
        }

        public void ShowMapping(Dictionary<string, string> fieldMapping)
        {
            foreach (var map in fieldMapping)
            {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Margin = new Thickness(10);
                stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
                Label label = new Label();
                label.Content = map.Key;
                stackPanel.Children.Add(label);
                TextBox textBox = new TextBox();
                textBox.Text = map.Value;
                stackPanel.Children.Add(textBox);
                ListBoxFieldMapping.Items.Add(stackPanel);
            }
        }

        public Dictionary<string, string> GetEditedMapping()
        {
            Dictionary<string, string> mapping = new Dictionary<string, string>();
            foreach (ItemCollection map in ListBoxFieldMapping.Items)
            {
                if (map.Count > 1 && map[0] is Label && map[1] is TextBox)
                    mapping.Add((map[0] as Label).Content.ToString(), (map[1] as TextBox).Text);
            }
            return mapping;
        }
    }
}

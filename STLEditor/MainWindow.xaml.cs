using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace STLEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string WindowTitle
        {
            get { return (string)GetValue(WindowTitleProperty); }
            set { SetValue(WindowTitleProperty, value); }
        }

        public static readonly DependencyProperty WindowTitleProperty =
            DependencyProperty.Register("WindowTitle", typeof(string), typeof(MainWindow), new UIPropertyMetadata(null));

        public MainWindow()
        {
            InitializeComponent();

            WindowTitle = "StringList Editor v0.2";
        }

        private void Open_btn(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                STLService.filename = openFileDialog.FileName;

                if (STLService.isFileValid())
                {

                    STLService.openFile();

                    List<DatagridEntry> entries = new List<DatagridEntry>();
                    foreach (_StlEntry entry in STLService.stringList.entries)
                    {
                        entries.Add(new DatagridEntry()
                        {
                            Key = entry.string1,
                            Value = entry.string2
                        });
                    }

                    WindowTitle = WindowTitle.Substring(0, 22) + " [" + openFileDialog.SafeFileName + "]";
                    dataGrid.ItemsSource = entries;
                }
            }
        }

        private void Save_btn(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save the file?",
                    "Save file",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                STLService.saveFile();
            }
        }

        private void SaveAs_btn(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                STLService.filename = saveFileDialog.FileName;
                WindowTitle = WindowTitle.Substring(0, 22) + " [" + saveFileDialog.SafeFileName + "]";

                STLService.saveFile();
            }
        }

        private void Close_btn(object sender, RoutedEventArgs e)
        {
            STLService.filename = null;
            STLService.stringList = null;
            dataGrid.ItemsSource = new List<DatagridEntry>();
            WindowTitle = WindowTitle.Substring(0, 22);
        }

        private void Exit_btn(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

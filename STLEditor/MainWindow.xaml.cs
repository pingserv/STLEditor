using Microsoft.Win32;
using STLEditor.Structs;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace STLEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool fileOpened = false;
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

            WindowTitle = "StringList Editor v0.3";
        }

        private void Open_btn(object sender, RoutedEventArgs e)
        {
            if (fileOpened)
            {
                STLService.closeFile();

                dataGrid.ItemsSource = new List<DatagridEntry>();
                WindowTitle = WindowTitle.Substring(0, 22);
                Statusbar.Items.Clear();
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                STLService.filename = openFileDialog.FileName;

                if (STLService.isFileValid())
                {
                    STLService.openFile();

                    WindowTitle = WindowTitle.Substring(0, 22) + " [" + openFileDialog.SafeFileName + "]";
                    dataGrid.ItemsSource = STLService.stringList.entries;

                    Statusbar.Items.Clear();
                    Statusbar.Items.Add(string.Concat(STLService.stringList.fileType, " file"));
                    Statusbar.Items.Add(string.Concat(STLService.stringList.entries.Count(), " records"));

                    fileOpened = true;
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
            if (fileOpened)
            {
                STLService.closeFile();

                dataGrid.ItemsSource = new List<DatagridEntry>();
                WindowTitle = WindowTitle.Substring(0, 22);
                Statusbar.Items.Clear();

                fileOpened = false;
            }
        }

        private void Exit_btn(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

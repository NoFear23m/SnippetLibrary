using SnippetLibrary.App.ViewModel.ViewViewModels;
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
using System.Windows.Shapes;

namespace SnippetLibrary.App.View.DialogViews
{
    /// <summary>
    /// Interaktionslogik für win_manageTagsAndLanguagesDialog.xaml
    /// </summary>
    public partial class win_manageLanguagesDialog : Window
    {
        ManageLanguagesViewModel VM { get; set; }
        
        public win_manageLanguagesDialog()
        {
            VM = new ManageLanguagesViewModel();
            DataContext = VM;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VM.Save();
            DialogResult = true;
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}

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
    /// Interaktionslogik für win_errorDialog.xaml
    /// </summary>
    public partial class win_errorDialog : Window
    {
        public win_errorDialog(string errorMessage, Exception ex)
        {
            DataContext = new ErrorViewModel(errorMessage, ex);
            InitializeComponent();            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}

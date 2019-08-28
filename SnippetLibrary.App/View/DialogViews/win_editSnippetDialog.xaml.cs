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

namespace SnippetLibrary.App.DialogViews
{
    /// <summary>
    /// Interaktionslogik für win_editSnippetDialog.xaml
    /// </summary>
    public partial class win_editSnippetDialog : Window
    {
        EditSnippetViewModel VM { get; set; }
        
        public win_editSnippetDialog()
        {
            InitializeComponent();
        }
        
        public win_editSnippetDialog(EditSnippetViewModel vm)
        {
            VM = vm;
            DataContext = VM;
            InitializeComponent();
        }
        
        public void Save_Execute(object sender, RoutedEventArgs e)
        {
            VM.Save();
            DialogResult = true;
            Close();
        }
        public void Cancel_Execute(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}

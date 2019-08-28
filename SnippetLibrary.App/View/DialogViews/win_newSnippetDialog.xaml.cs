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
    /// Interaktionslogik für win_newSnippetDialog.xaml
    /// </summary>
    public partial class win_newSnippetDialog : Window
    {
        
        AddSnippetViewModel VM { get; set; }
        
        public win_newSnippetDialog()
        {
            InitializeComponent();
        }

        public win_newSnippetDialog(AddSnippetViewModel vm)
        {
            VM = vm;
            DataContext = VM;
            InitializeComponent();
        }
        
        public void AddSnippet_Execute(object sender, RoutedEventArgs e)
        {
            VM.Add();
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

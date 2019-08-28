using SnippetLibrary.App.ViewModel.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SnippetLibrary.App.ViewModel.ViewViewModels
{
    public class ErrorViewModel : ViewModelBase
    {
        public ErrorViewModel(string errorMessage, Exception ex)
        {
            ErrorText = errorMessage;
            this.ex = ex;
        }

        private string _ErrorText;

        public string ErrorText
        {
            get { return _ErrorText + "\nInfo: " + ex.Message; }
            set { _ErrorText = value; RaisePropertyChanged(); }
        }


        private Exception ex;





        private ICommand _Close;
        public ICommand CloseCommand
        {
            get
            {
                if (_Close == null)
                {
                    _Close = new RelayCommand(x => Environment.Exit(0));
                }
                return _Close;
            }
        }

        private ICommand _Mail;
        public ICommand MailCommand
        {
            get
            {
                if (_Mail == null)
                {
                    MessageBox.Show(ex.Message);
                    _Mail = new RelayCommand(x => Process.Start("mailto:florian.programming@gmail.com?subject=SnippetLibrary Error&body=" + _ErrorText));
                }
                return _Mail;
            }
        }
    }
}

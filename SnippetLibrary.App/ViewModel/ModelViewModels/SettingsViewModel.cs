using SnippetLibrary.App.Model;
using SnippetLibrary.App.ViewModel.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnippetLibrary.App.ViewModel.ModelViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public Settings Settings_Model { get; set; }
        
        public SettingsViewModel(Settings model)
        {
            Settings_Model = model;
        }


        private bool _ShowIdUnderSnippetName;

        public bool ShowIdUnderSnippetName
        {
            get { return _ShowIdUnderSnippetName; }
            set { _ShowIdUnderSnippetName = value; Settings_Model.ShowIdUnderSnippetName = value; RaisePropertyChanged(); }
        }

        private string _DefaultCreatedBY;

        public string DefaultCreatedBy
        {
            get { return _DefaultCreatedBY; }
            set { _DefaultCreatedBY = value; Settings_Model.DefaultCreatedBy = value; RaisePropertyChanged(); }
        }
        
    }
}

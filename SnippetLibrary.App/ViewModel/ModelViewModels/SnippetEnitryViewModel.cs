using SnippetLibrary.App.Model;
using SnippetLibrary.App.ViewModel.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnippetLibrary.App.ViewModel.ModelViewModels
{

    public class SnippetEnitryViewModel : ViewModelBase
    {
        public SnippetEnitry SnippetEnitry_Model { get; set; }
        public SnippetEnitryViewModel(SnippetEnitry model, List<LanguageViewModel> aviableDatas)
        {
            SnippetEnitry_Model = model;

            Name = model.Name;
            SnippetText = model.SnippetText;

            AviableLanguages = aviableDatas;
            _Language = aviableDatas.Where(x => x.ID == model.LanguageID).SingleOrDefault();
        }

        
        public List<LanguageViewModel> AviableLanguages { get; }
        
        private LanguageViewModel _Language;
        public LanguageViewModel Language
        {
            get { return _Language; }
            set { _Language = value; SnippetEnitry_Model.LanguageID = value.ID; RaisePropertyChanged(); }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; SnippetEnitry_Model.Name = Name; RaisePropertyChanged(); }
        }

        private string _SnippetText;
        public string SnippetText
        {
            get { return _SnippetText; }
            set { _SnippetText = value; SnippetEnitry_Model.SnippetText = SnippetText; RaisePropertyChanged(); }
        }


    }

}

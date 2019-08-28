using SnippetLibrary.App.Model;
using SnippetLibrary.App.Model.SnippetProperties;
using SnippetLibrary.App.Other;
using SnippetLibrary.App.ViewModel.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnippetLibrary.App.ViewModel.ModelViewModels
{

    public class LanguageViewModel : ViewModelBase
    {
        public Language Language_Model { get; set; }
        public LanguageViewModel(Language model)
        {
            Language_Model = model;

            _ID = model.ID;
            Name = model.Name;
        }

        private Guid _ID;
        public Guid ID
        {
            get { return _ID; }
        }


        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; Language_Model.Name = Name; RaisePropertyChanged(); }
        }
        
        public int UseCount
        {
            get
            {
                int usecount = 0;
                foreach (Snippet s in DataService.Instance.Snippets)
                {
                    if (s.LanguageID == ID) usecount++;
                    foreach (SnippetEnitry enitry in s.SnippetEnitries)
                    {
                        if (enitry.LanguageID == ID) usecount++;
                    }
                }
                return usecount;

            }
        }
    }
}

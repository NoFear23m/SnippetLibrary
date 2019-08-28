using SnippetLibrary.App.Other;
using SnippetLibrary.App.ViewModel.Infrastructure;
using SnippetLibrary.App.ViewModel.ModelViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnippetLibrary.App.ViewModel.ViewViewModels
{
    public class AddSnippetViewModel : ViewModelBase
    {

        public AddSnippetViewModel()
        {
            List<LanguageViewModel> lVM = new List<LanguageViewModel>() ;  DataService.Instance.Languages.ForEach(x => lVM.Add(new LanguageViewModel(x)));
            Model.Snippet sModel = new Model.Snippet(); sModel.Name = ""; sModel.LanguageID = lVM.FirstOrDefault().ID;
            sModel.SnippetEnitries = new List<Model.SnippetEnitry>();
            sModel.CreatedBy = Environment.UserName;
            Snippet = new SnippetViewModel(sModel, lVM);
            Snippet.CreatedAt = DateTime.Now;
        }

        private SnippetViewModel _Snippet;
        public SnippetViewModel Snippet
        {
            get { return _Snippet; }
            set { _Snippet = value; RaisePropertyChanged(); }
        }
        
        
        public void Add()
        {
            Snippet.UpdateModel();
            DataService.Instance.Snippets.Add(Snippet.Snippet_Model);
        }
        
    }
}

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

    public class TagViewModel : ViewModelBase
    {
        public Tag Tag_Model { get; set; }
        public TagViewModel(Tag model)
        {
            Tag_Model = model;

            Name = model.Name;
        }


        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; Tag_Model.Name = Name; RaisePropertyChanged(); }
        }

    }

}

using SnippetLibrary.App.Model;
using SnippetLibrary.App.ViewModel.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnippetLibrary.App.ViewModel.ModelViewModels
{
    public class SnippetViewModel : ViewModelBase
    {

        public Snippet Snippet_Model { get; set; }
        public SnippetViewModel(Snippet model, List<LanguageViewModel> aviableDatas)
        {
            Tags = new ObservableCollection<TagViewModel>();
            SnippetEnitries = new ObservableCollection<SnippetEnitryViewModel>();

            Snippet_Model = model;

            _ID = model.ID;
            _Name = model.Name;

            model.SnippetEnitries.ForEach(x => SnippetEnitries.Add(new SnippetEnitryViewModel(x, aviableDatas)));
            model.Tags.ForEach(x => Tags.Add(new TagViewModel(x)));

            AviableLanguages = aviableDatas;
            Language = AviableLanguages.Where(x => x.ID == model.LanguageID).SingleOrDefault();

            

            CreatedAt = model.CreatedAt;
            CreatedBy = model.CreatedBy;

            SelectedSnippetEnitry = SnippetEnitries.FirstOrDefault();

        }


        private Guid _ID;
        public Guid ID
        {
            get { return _ID; }
        }


        private ObservableCollection<SnippetEnitryViewModel> _SnippetEnitries;
        public ObservableCollection<SnippetEnitryViewModel> SnippetEnitries
        {
            get { return _SnippetEnitries; }
            set
            {
                _SnippetEnitries = value;
                UpdateModel();
                RaisePropertyChanged();
            }
        }

        private SnippetEnitryViewModel _SelectedSnippetEnitry;
        public SnippetEnitryViewModel SelectedSnippetEnitry
        {
            get { return _SelectedSnippetEnitry; }
            set { _SelectedSnippetEnitry = value; RaisePropertyChanged(); }
        }



        private ObservableCollection<TagViewModel> _Tags;
        public ObservableCollection<TagViewModel> Tags
        {
            get { return _Tags; }
            set
            {
                _Tags = value;
                UpdateModel();
                RaisePropertyChanged();
            }
        }
        
        public void UpdateModel()
        {
            if (Snippet_Model != null)
            {
                Debug.WriteLine("Snippet_Model != null");
                Snippet_Model.Tags.Clear();
                _Tags.ToList().ForEach(x => Snippet_Model.Tags.Add(x.Tag_Model));

                Snippet_Model.SnippetEnitries.Clear();
                _SnippetEnitries.ToList().ForEach(x => Snippet_Model.SnippetEnitries.Add(x.SnippetEnitry_Model));
            }
            else
            {
                Debug.WriteLine("Snippet_Model == null!");
            }
        }

        public string TagString
        {
            get
            {
                if (Tags.Count == 0)
                {
                    return "--keine--";
                }
                else
                {
                    string r = Tags[0].Name;
                    for (int i = 1; i < Tags.Count; i++)
                    {
                        r += ", " + Tags[i].Name;
                    }
                    return r;
                }
            }

            set
            {
                Tags.Clear();
                var tagEntered = value.Split(',');
                foreach (string tagString in tagEntered)
                {
                    string tagStirngNew = tagString;
                    if (tagString != "--keine--")
                    {
                         if (tagString[0] == ' ' ) {tagStirngNew = tagString.Remove(0, 1); }
                           Model.SnippetProperties.Tag newTag = new Model.SnippetProperties.Tag { Name = tagStirngNew };
                        Tags.Add(new TagViewModel(newTag));
                    }
                   
                }
            }
        }


        public List<LanguageViewModel> AviableLanguages { get; }
        private LanguageViewModel _Language;
        public LanguageViewModel Language
        {
            get { return _Language; }
            set { _Language = value; Snippet_Model.LanguageID = value.ID;  RaisePropertyChanged(); }
        }


        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; Snippet_Model.Name = Name; RaisePropertyChanged(); }
        }



        private DateTime _CreatedAt;
        public DateTime CreatedAt
        {
            get { return _CreatedAt; }
            set { _CreatedAt = value; Snippet_Model.CreatedAt = _CreatedAt; RaisePropertyChanged(); }
        }

        private string _CreatedBy;
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; Snippet_Model.CreatedBy = CreatedBy; RaisePropertyChanged(); }
        }
    }
}

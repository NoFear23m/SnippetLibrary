using Microsoft.Win32;
using SnippetLibrary.App.DialogViews;
using SnippetLibrary.App.Model;
using SnippetLibrary.App.Model.SnippetProperties;
using SnippetLibrary.App.Other;
using SnippetLibrary.App.View.DialogViews;
using SnippetLibrary.App.ViewModel.Infrastructure;
using SnippetLibrary.App.ViewModel.ModelViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace SnippetLibrary.App.ViewModel.ViewViewModels
{

    public class OverviewViewModel : ViewModelBase
    {

        #region Constructors
        public OverviewViewModel()
        {
            IsFileOpen = false;

            AllSnippets = new ObservableCollection<SnippetViewModel>();

            SelectedSnippet = AllSnippets.FirstOrDefault();

            AllSnippetsView = CollectionViewSource.GetDefaultView(AllSnippets);
            AllSnippetsView.Filter = AllSnippetsView_Filter;
            AllSnippetsView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            AllSnippetsView.GroupDescriptions.Add(new PropertyGroupDescription("Language"));



        }
        #endregion

        #region Fields
        private ObservableCollection<SnippetViewModel> _AllSnippets;

        private ICollectionView _AllSnippetsView;

        //Filter:
        private string _FilterText;

        private bool _IsFileOpen;

        private SnippetViewModel _SelectedSnippet;

        //Methoden
        private string _OpenFileName = string.Empty;
        #endregion

        #region Properties
        public ObservableCollection<SnippetViewModel> AllSnippets
        {
            get { return _AllSnippets; }
            set { _AllSnippets = value; RaisePropertyChanged(); }
        }
        public SnippetViewModel SelectedSnippet
        {
            get { return _SelectedSnippet; }
            set { _SelectedSnippet = value; RaisePropertyChanged(); }
        }

        public ICollectionView AllSnippetsView
        {
            get { return _AllSnippetsView; }
            set { _AllSnippetsView = value; RaisePropertyChanged(); }
        }

        public string FilterText
        {
            get { return _FilterText; }
            set { _FilterText = value; AllSnippetsView.Refresh(); RaisePropertyChanged(); }
        }

        
        //Andere
        
        public string OpenFileName
        {
            get { return _OpenFileName; }
            set { _OpenFileName = value; RaisePropertyChanged(); RaisePropertyChanged("WindowTitle"); }

        }
        public bool IsFileOpen
        {
            get { return _IsFileOpen; }
            set { _IsFileOpen = value; RaisePropertyChanged(); }
        }
        
        public string WindowTitle
        {
            get
            {
                string s = "Snippet Library";
                if (IsFileOpen)
                {
                    s += " - " + new FileInfo(OpenFileName).Name;
                }
                return s;          
            }
        }
        #endregion

        #region Private Methods
        private bool AllSnippetsView_Filter(object obj)
        {
            if (String.IsNullOrEmpty(FilterText)) return true;

            SnippetViewModel currentSnippet = (SnippetViewModel)obj;
            string lowerFilterText = FilterText.ToLower();

            if (currentSnippet.Name.ToLower().Contains(lowerFilterText)) return true;
            if (currentSnippet.Language.Name.ToLower().Contains(lowerFilterText)) return true;
            if (currentSnippet.Tags.ToList().Where(x => x.Name.ToLower().Contains(lowerFilterText)).Count() > 0) return true;

            return false;
        }
        #endregion

        #region Methods
        //Methods
        public void Load(string FileName = null)
        {
            if (FileName != null)
            {
                DataService.Instance.ClearAllData();
                DataService.Instance.LoadFromFile(new System.IO.FileInfo(FileName));
                IsFileOpen = true;
                OpenFileName = FileName;
            }

            List<LanguageViewModel> languagesVM = new List<LanguageViewModel>();
            List<SnippetViewModel> snippetsVM = new List<SnippetViewModel>();

            DataService.Instance.Languages.ForEach(x => languagesVM.Add(new LanguageViewModel(x)));
            
            DataService.Instance.Snippets.ForEach(x => snippetsVM.Add(new SnippetViewModel(x, languagesVM)));

            AllSnippets = new ObservableCollection<SnippetViewModel>();

            snippetsVM.ForEach(x => AllSnippets.Add(x));

            SelectedSnippet = AllSnippets.FirstOrDefault();

            AllSnippetsView = CollectionViewSource.GetDefaultView(AllSnippets);
            AllSnippetsView.Filter = AllSnippetsView_Filter;
            AllSnippetsView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            AllSnippetsView.GroupDescriptions.Add(new PropertyGroupDescription("Language"));

        }
        
        
        //Öffnen, speichern, neu
        
        public void NewFile()
        {

            SaveFileDialog sfd = new SaveFileDialog
            {
                Title = "Neue Datei anlegen...",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "SnippetDataBase *sdb|*.sdb"
            };
            
            if (sfd.ShowDialog() == true)
            {
                DataService.Instance.ClearAllData();

                //Sprachen einspeichern:
                Language vb = new Language { ID = Guid.NewGuid(), Name = "VB" }; Language cs = new Language { ID = Guid.NewGuid(), Name = "C#" }; Language html = new Language { ID = Guid.NewGuid(), Name = "HTML" }; Language css = new Language { ID = Guid.NewGuid(), Name = "CSS" };
                List<Language> languages = new List<Language> { vb, cs,html,css };
                DataService.Instance.Languages = languages;
                DataService.Instance.SaveToFile(new System.IO.FileInfo(sfd.FileName));
                Load(sfd.FileName);
            }

        }
        public void OpenFile()
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "Snippet Library öffnen...",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "SnippetDataBase *sdb|*.sdb"
            };

            if (ofd.ShowDialog() == true)
            {
                Load(ofd.FileName);
            }
        }
        public void SaveFile()
        {
            System.IO.File.Delete(OpenFileName);
            DataService.Instance.SaveToFile(new System.IO.FileInfo(OpenFileName));
        }
        public void SaveFileAs()
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Title = "Datei speichern als...",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),  
                Filter = "SnippetDataBase *sdb|*.sdb"
            };
            
            if (sfd.ShowDialog() == true)
            {
                DataService.Instance.SaveToFile(new System.IO.FileInfo(sfd.FileName));
                Load(sfd.FileName);
            }        
        }
        
        

        // Löschen, Bearbeiten, Neues Snippet
        
        private void NewSnippet()
        {
            win_newSnippetDialog dialog = new win_newSnippetDialog(new AddSnippetViewModel());
            if (dialog.ShowDialog() == true)
            {
                Load(null);
            }
        }    
            
            
        private void EditSnippet() //nur verfügbar, wenn SelectedSnippet != null ist
        {
            win_editSnippetDialog dialog = new win_editSnippetDialog(new EditSnippetViewModel(SelectedSnippet.ID));
            if( dialog.ShowDialog() == true)
            {
                Load(null);
            }
        }
        private void DeleteSnippet() //nur verfügbar, wenn SelectedSnippet != null ist
        {
            if (MessageBox.Show("Wollen sie den Snippet Eintrag wirklich löschen?", "Warnung", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                AllSnippets.Remove(AllSnippets.FirstOrDefault(x => x.ID == SelectedSnippet.ID));
                DataService.Instance.Snippets = new List<Snippet>();
                AllSnippets.ToList().ForEach(x => DataService.Instance.Snippets.Add(x.Snippet_Model));
                SelectedSnippet = AllSnippets.FirstOrDefault();
            }           
        }

        public MessageBoxResult AskForSaveFile()
        {
            return MessageBox.Show("Möchten sie die Änderungen speichern?", "Warnung", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
        }

        
        private void TagAndLanguageManager()
        {
            win_manageLanguagesDialog dialog = new win_manageLanguagesDialog();
            if (dialog.ShowDialog() == true)
            {
                Load(null);
            }
        }

        #endregion

        //Commands


        #region Commands


        private ICommand _TagAndLanguageManager;
        public ICommand TagAndLanguageManagerCommand
        {
            get
            {
                if (_TagAndLanguageManager == null)
                {
                    _TagAndLanguageManager = new RelayCommand(x => TagAndLanguageManager(), x=> IsFileOpen);
                }
                return _TagAndLanguageManager;
            }
        }


        private ICommand _NewFile;
        public ICommand NewFileCommand
        {
            get
            {
                if (_NewFile == null)
                {
                    _NewFile = new RelayCommand(x => NewFile());
                }
                return _NewFile;
            }
        }


        private ICommand _OpenFile;
        public ICommand OpenFileCommand
        {
            get
            {
                if (_OpenFile == null)
                {
                    _OpenFile = new RelayCommand(x => OpenFile());
                }
                return _OpenFile;
            }
        }

        private ICommand _SaveFile;
        public ICommand SaveFileCommand
        {
            get
            {
                if (_SaveFile == null)
                {
                    _SaveFile = new RelayCommand(x => SaveFile(), x => IsFileOpen);
                }
                return _SaveFile;
            }
        }

        private ICommand _SaveFileAs;
        public ICommand SaveFileAsCommand
        {
            get
            {
                if (_SaveFileAs == null)
                {
                    _SaveFileAs = new RelayCommand(x => SaveFileAs(), x => IsFileOpen);
                }
                return _SaveFileAs;
            }
        }




        private ICommand _NewSnippet;
        public ICommand NewSnippetCommand
        {
            get
            {
                if (_NewSnippet == null)
                {
                    _NewSnippet = new RelayCommand(x => NewSnippet(), x=> IsFileOpen);
                }
                return _NewSnippet;
            }
        }

        private ICommand _EditSnippet;
        public ICommand EditSnippetCommand
        {
            get
            {
                if (_EditSnippet == null)
                {
                    _EditSnippet = new RelayCommand(x => EditSnippet(), x => SelectedSnippet != null);
                }
                return _EditSnippet;
            }
        }
        private ICommand _DeleteSnippet;
        public ICommand DeleteSnippetCommand
        {
            get
            {
                if (_DeleteSnippet == null)
                {
                    _DeleteSnippet = new RelayCommand(x => DeleteSnippet(), x => SelectedSnippet != null);
                }
                return _DeleteSnippet;
            }
        }

        private ICommand _CloseApplication;
        public ICommand CloseApplicationCommand
        {
            get
            {
                if (_CloseApplication == null)
                {
                    _CloseApplication = new RelayCommand(x => Environment.Exit(0));
                }
                return _CloseApplication;
            }
        }

        private ICommand _ShowAboutDialog;
        public ICommand ShowAboutDialogCommand
        {
            get
            {
                if (_ShowAboutDialog == null)
                {
                    _ShowAboutDialog = new RelayCommand(x => ShowAboutDialgoCommand_Execute());
                }
                return _ShowAboutDialog;
            }
        }
        
        private ICommand _FirstHelp;
        public ICommand FirstHelpCommand
        {
            get
            {
                if (_FirstHelp == null)
                {
                    _FirstHelp = new RelayCommand(x => FirstHelpCommand_Execute());
                }
                return _FirstHelp;
            }
        }
        private void FirstHelpCommand_Execute()
        {
            HelpViewModel vm = new HelpViewModel("Allgemeine Bedinung","Um eine neue Datei anzulegen gehen sie auf Datei>Neu\nUm einer geöffneten Datei ein Snippet hinzuzufügen, gehen sie auf Snippet>Neu");
            win_helpDialog dialog = new win_helpDialog(vm); dialog.ShowDialog();
        }


        private ICommand _CopyToClipboard;
        public ICommand CopyToClipboardCommand
        {
            get
            {
                if (_CopyToClipboard == null)
                {
                    _CopyToClipboard = new RelayCommand(x => Clipboard.SetText(SelectedSnippet.SelectedSnippetEnitry.SnippetText), x=> SelectedSnippet != null && SelectedSnippet.SelectedSnippetEnitry != null);
                }
                return _CopyToClipboard;
            }
        }

        private void ShowAboutDialgoCommand_Execute()
        {
            win_about dialog = new win_about();
            dialog.ShowDialog();
        }


        #endregion

    }
}

using SnippetLibrary.App.ViewModel.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnippetLibrary.App.ViewModel.ViewViewModels
{

    public class AboutViewModel : ViewModelBase
    {
        public AboutViewModel()
        {
            if (!IsInDesignMode)
            {
                AllAssemblys = new List<AssemblyListItem>();
                AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic).ToList().ForEach(x => AllAssemblys.Add(new AssemblyListItem(x.Location)));
            }
            AllUsedLibraries = new List<UsedLibrariesItem>();
            AllUsedLibraries.Add(new UsedLibrariesItem("AvalonEdit", "Ein Texteditor mit Syntaxhighlithing", "5.0.3.0"));
            AllUsedLibraries.Add(new UsedLibrariesItem("Xcreed WPF Toolkit", "Einige WPF Controls", "3.5.0.0"));
        }


        private List<AssemblyListItem> _allAssemblys;
        public List<AssemblyListItem> AllAssemblys
        {
            get
            {
                return _allAssemblys;
            }
            set
            {
                _allAssemblys = value;
                RaisePropertyChanged();
            }
        }



        private List<UsedLibrariesItem> _allUsedLibraries;
        public List<UsedLibrariesItem> AllUsedLibraries
        {
            get
            {
                return _allUsedLibraries;
            }
            set
            {
                _allUsedLibraries = value;
                RaisePropertyChanged();
            }
        }


        public string AppName
        {
            get
            {
                return "SnippetLibrary";
            }
        }

        public string AppDescription
        {
            get
            {
                return "Ein simples Programm um CodeSnippets zu kategorisieren, speichern, sortieren und suchen.";
            }
        }

        public string Version
        {
            get
            {
                return "1.0.0.0";
            }
        }

        public string Copyright
        {
            get
            {
                return $"(c) 2019 by Florian";
            }
        }
    }

    public class AssemblyListItem
    {
        public AssemblyListItem()
        {
        }

        public AssemblyListItem(string location)
        {
            var fvi = FileVersionInfo.GetVersionInfo(location);
            Name = fvi.InternalName;
            Description = fvi.FileDescription;
            Version = fvi.ProductVersion;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
    }

    public class UsedLibrariesItem
    {
        public UsedLibrariesItem()
        {
        }

        public UsedLibrariesItem(string name, string description, string version)
        {

            Name = name;
            Description = description;
            Version = version;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
    }
}

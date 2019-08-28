using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using SnippetLibrary.App.ViewModel.ViewViewModels;
using SnippetLibrary.App.WorkspaceViews;

namespace SnippetLibrary.App
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            if (e.Args.Length > 0 && File.Exists(e.Args[0]))
            {
                FileInfo fileInfo = new FileInfo(e.Args[0]);
                if (fileInfo.Extension == ".sdb")
                {
                    OverviewViewModel vm = new OverviewViewModel();
                    vm.Load(fileInfo.FullName);
                    win_mainWorkspace workspace = new win_mainWorkspace(vm);
                    workspace.Show();
                }
                else
                {
                    OverviewViewModel vm = new OverviewViewModel();
                    win_mainWorkspace workspace = new win_mainWorkspace(vm);
                    workspace.Show();
                }
            }
            else
            {
                OverviewViewModel vm = new OverviewViewModel();
                win_mainWorkspace workspace = new win_mainWorkspace(vm);
                workspace.Show();
            }
        }
    }




}

using SnippetLibrary.App.Model;
using SnippetLibrary.App.Model.SnippetProperties;
using SnippetLibrary.App.View.DialogViews;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SnippetLibrary.App.Other
{

    public class DataService
    {
        public List<Language> Languages { get; set; }
        public List<Snippet> Snippets { get; set; }


        private DataService()  // Damit kein Objekt angelegt werden kann
        {
        }

        public static DataService Instance { get; } = new DataService();

        // Properties speichern



        public void SaveToFile(FileInfo SaveFile)
        {
            try
            {
                if (!Directory.Exists(SaveFile.Directory.FullName)) throw new FileNotFoundException(); //Wenn Ordner nicht vorhanden ist.

                //Temp Verzeichnis erstellen
                string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\tempSave";
                if (Directory.Exists(tempPath)) Directory.Delete(tempPath, true); //Sichergehen, dass das Verzeichnis nicht exisitiert.
                Directory.CreateDirectory(tempPath);

                //Daten speichern
                Serializer.SaveXML(tempPath + "\\languages.slf", Languages);
                Serializer.SaveXML(tempPath + "\\snippets.slf", Snippets);

                //Daten zu einer Datei packen und in das Verzeichnís kopieren
                ZipFile.CreateFromDirectory(tempPath, SaveFile.FullName);
            }
            catch (Exception ex)
            {
                win_errorDialog errorDialog = new win_errorDialog("Fehler beim speichern der Datei. Nehmen sie Kontakt mit dem Entwickler auf.", ex);
                errorDialog.ShowDialog();
            }


        }

        public void LoadFromFile(FileInfo OpenFile)
        {
            try
            {
                if (!Directory.Exists(OpenFile.Directory.FullName)) throw new FileNotFoundException(); //Wenn Ordner nicht vorhanden ist.

                //Temp Verzeichnis erstellen
                string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\tempLoad";
                if (Directory.Exists(tempPath)) Directory.Delete(tempPath, true); //Sichergehen, dass das Verzeichnis nicht exisitiert.
                Directory.CreateDirectory(tempPath);

                ZipFile.ExtractToDirectory(OpenFile.FullName, tempPath);

                Languages = Serializer.LoadXML<List<Language>>(tempPath + "\\languages.slf");
                Snippets = Serializer.LoadXML<List<Snippet>>(tempPath + "\\snippets.slf");
            }
            catch (Exception ex)
            {
                win_errorDialog errorDialog = new win_errorDialog("Fehler beim Öffnen der Datei. Bitte überprüfen sie, ob die Datei beschädigt ist.", ex);
                errorDialog.ShowDialog();
            }

        }

        public void ClearAllData()
        {
            Languages = new List<Language>();
            Snippets = new List<Snippet>();
        }


        
        // Private Methoden und Funktionen

    }

    
    
    
    
    
    public static class Serializer
    {
        public static void SaveXML<K>(string filename, K serializedObject)
        {
            try
            {
                XmlSerializer xml = new XmlSerializer(typeof(K));
                FileStream fs = new FileStream(filename, FileMode.Create);
                xml.Serialize(fs, serializedObject);
                fs.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static K LoadXML<K>(string filename)
        {
            try
            {
                XmlSerializer xml = new XmlSerializer(typeof(K));
                K ret;
                FileStream fs = new FileStream(filename, FileMode.Open);
                ret = (K)xml.Deserialize(fs);
                fs.Close();
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

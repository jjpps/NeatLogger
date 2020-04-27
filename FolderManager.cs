using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeatLogger
{
    internal class FolderManager
    {
        string appName = System.AppDomain.CurrentDomain.FriendlyName.ToString().Substring(0,System.AppDomain.CurrentDomain.FriendlyName.Length - 4);
        string path = string.Empty;
        string folder = string.Empty;
        string nameFile = string.Empty;
        public FolderManager(string path, string folder,string filename)
        {
            this.path = path;
            this.folder = folder;
            this.nameFile = filename;
            CreateParentFolder();
        }
        private void CreateParentFolder()
        {
            if (Directory.Exists($"{path}\\{appName}"))
                return;
            else
            {
                Directory.CreateDirectory($"{path}\\{appName}");                
            }
        }
        public bool CheckExists()
        {
            if (Directory.Exists(MontaPath()))
                return true;
            else
            {
                Directory.CreateDirectory(MontaPath());
                return Directory.Exists(MontaPath());
            }
        }
        private string MontaPath()
        {
            return $"{path}\\{appName}\\{folder}";
        }
        public string MontaPathFile()
        {
            return $"{path}\\{appName}\\{folder}\\{nameFile}";
        }


    }
}

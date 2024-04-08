using ETıcaretAPI.Infrastructure.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Infrastructure.Services.Storage
{
    public class Storage
    {
        protected string FileRename(string path, string fileName)
        {
            string extension = Path.GetExtension(fileName);
            string oldName = Path.GetFileNameWithoutExtension(fileName);
            string newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";
            string fullPath = $"{path}\\{newFileName}";
            int i = 1;
            while (File.Exists(fullPath))
            {
                newFileName = $"{NameOperation.CharacterRegulatory(oldName)}-{i}{extension}";
                fullPath = $"{path}\\{newFileName}";
                i++;
            }
            return newFileName; 
        }
    }
}

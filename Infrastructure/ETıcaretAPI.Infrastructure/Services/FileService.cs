using ETıcaretAPI.Application.Services;
using ETıcaretAPI.Infrastructure.Operations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> CopyFileAsyn(string fullPath, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex )
            {
                //todo log!
                throw ex;
            }
            
        }

        private string FileRenameAsync(string path,string fileName)
        {
            string extension = Path.GetExtension(fileName);
            string oldName= Path.GetFileNameWithoutExtension(fileName);
            string newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";
            string fullPath = $"{path}\\{newFileName}";
            int i = 1;
            while(File.Exists(fullPath))
            {
                newFileName = $"{NameOperation.CharacterRegulatory(oldName)}-{i}{extension}";
                fullPath =  $"{path}\\{newFileName}";
                i++;
            }
            return newFileName;
        }

        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
        {
            
            string uploadPath=Path.Combine(_webHostEnvironment.WebRootPath, path);

            List<(string fileName, string path)> datas = new();

            if(!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<bool> results = new();
            foreach(IFormFile file in files)
            {
              string fileNewName=   FileRenameAsync(uploadPath,file.FileName);

               bool result= await CopyFileAsyn(Path.Combine(uploadPath, fileNewName), file);
                datas.Add((fileNewName, Path.Combine(uploadPath, fileNewName)));
                results.Add(result);
            }

            if (results.TrueForAll(r => r.Equals(true)))
                return datas;

            //todo Eğer ki yukarıdaki if geçerli değilse burada dosyaların sunucuda yüklenirken hata alındığına dair sunucuda hata alındığına dair bir exception oluşturup fırlat
            return null;
        }
    }
}

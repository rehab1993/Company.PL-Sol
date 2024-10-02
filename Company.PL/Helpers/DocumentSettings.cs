using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Company.PL.Helpers
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string FolderName)
        {
            string FolderPath =Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Files" ,FolderName);
            string FileName =$"{ Guid.NewGuid()}{file.FileName}";
            string FilePath = Path.Combine(FolderPath,FileName);
           using var FS = new FileStream(FilePath , FileMode.Create);
            file.CopyTo(FS);
            return FileName;

        }

    }
}

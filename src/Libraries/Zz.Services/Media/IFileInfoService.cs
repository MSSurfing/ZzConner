using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zz.Core.Data.Entity.Media;

namespace Zz.Services.Media
{
    public interface IFileInfoService
    {
        string GetExtensionById(Guid Id);

        FileInfo GetById(Guid Id);

        void Delete(FileInfo entity);

        void Insert(FileInfo entity);

        void Update(FileInfo entity);


        #region Store or Load in file system
        byte[] LoadBinary(FileInfo entity);

        byte[] GetBinary(IFormFile formFile);

        void SaveInFile(FileInfo entity);

        FileInfo SaveInFile(IFormFile formFile);


        #endregion
    }
}

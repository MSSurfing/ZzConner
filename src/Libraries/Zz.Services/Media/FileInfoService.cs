using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Engine;
using System;
using System.Collections.Generic;
using IO = System.IO;
using System.Linq;
using System.Threading.Tasks;
using Zz.Core.Data.Entity.Media;
using Autofac.Engine;
using Microsoft.AspNetCore.Hosting;

namespace Zz.Services.Media
{
    public partial class FileInfoService : IFileInfoService
    {
        #region Consts
        private const string FILE_DIRECTORY_PATH = "~/Assets/Assemblies/";
        private readonly string RootPath = "";
        #endregion

        #region Fields

        private readonly IRepository<FileInfo> _fileInfoRepository;
        #endregion

        #region Ctor
        public FileInfoService(IRepository<FileInfo> fileInfoRepository)
        {
            _fileInfoRepository = fileInfoRepository;
            RootPath = MapPath(FILE_DIRECTORY_PATH);
        }

        #endregion

        #region Utilities
        protected virtual byte[] LoadFileBinary(FileInfo file)
        {
            //Todo
            return new byte[0];
        }

        protected virtual bool IsUncPath(string path)
        {
            return Uri.TryCreate(path, UriKind.Absolute, out var uri) && uri.IsUnc;
        }

        protected virtual string MapPath(string virtualPath)
        {
            virtualPath = virtualPath.Replace("~/", string.Empty).TrimStart('/');
            var pathEnd = virtualPath.EndsWith('/') ? IO.Path.DirectorySeparatorChar.ToString() : string.Empty;

            var webHost = EngineContext.Resolve<IWebHostEnvironment>();
            var rootPath = webHost.ContentRootPath;

            return Combine(rootPath ?? string.Empty, virtualPath) + pathEnd;
        }

        protected string Combine(params string[] paths)
        {
            var path = IO.Path.Combine(paths.SelectMany(p => IsUncPath(p) ? new[] { p } : p.Split('\\', '/')).ToArray());

            if (Environment.OSVersion.Platform == PlatformID.Unix && !IsUncPath(path))
                path = "/" + path;

            return path;
        }
        #endregion

        #region Methods
        public virtual string GetExtensionById(Guid Id)
        {
            if (Id == Guid.Empty)
                return null;

            var query = from o in _fileInfoRepository.Table
                        where o.Id == Id
                        select o.Extension;

            return query.FirstOrDefault();
        }

        public virtual FileInfo GetById(Guid Id)
        {
            if (Id == Guid.Empty)
                return null;

            return _fileInfoRepository.GetById(Id);
        }

        public virtual void Delete(FileInfo FileInfo)
        {
            if (FileInfo == null)
                throw new ArgumentNullException("FileInfo");

            _fileInfoRepository.Delete(FileInfo);
        }

        public virtual void Insert(FileInfo FileInfo)
        {
            if (FileInfo == null)
                throw new ArgumentNullException("FileInfo");

            _fileInfoRepository.Insert(FileInfo);
        }

        public virtual void Update(FileInfo FileInfo)
        {
            if (FileInfo == null)
                throw new ArgumentNullException("FileInfo");

            _fileInfoRepository.Update(FileInfo);
        }
        #endregion

        #region Store or Load in file system
        public virtual byte[] LoadBinary(FileInfo FileInfo)
        {
            return LoadFileBinary(FileInfo);
        }

        public virtual byte[] GetBinary(IFormFile formFile)
        {
            using var fileStream = formFile.OpenReadStream();
            using var ms = new IO.MemoryStream();
            fileStream.CopyTo(ms);
            var fileBytes = ms.ToArray();

            return fileBytes;
        }

        public virtual void SaveInFile(FileInfo entity)
        {
            // Todo
        }

        public virtual FileInfo SaveInFile(IFormFile formFile)
        {
            // 由外部验证??
            if (formFile == null)
                throw new ArgumentNullException(nameof(formFile));

            var fileName = formFile.FileName;
            if (string.IsNullOrEmpty(fileName))
                fileName = Guid.NewGuid().ToString();

            var extension = IO.Path.GetExtension(fileName);

            try
            {
                var path = Combine(RootPath, fileName);
                using var createStream = new IO.FileStream(path, IO.FileMode.Create);
                formFile.CopyTo(createStream);

                var entity = new FileInfo()
                {
                    Filename = fileName,
                    Extension = extension,
                    ContentType = "",
                    Deleted = false,
                    Path = path,
                };

                return entity;
            }
            catch (Exception ex) { }
            //catch { }

            return null;
        }
        #endregion
    }
}

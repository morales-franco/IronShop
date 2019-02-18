using IronShop.Api.Core.Entities.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.IServices
{
    public interface IFileService
    {
        Task<IronFile> DownloadAndSaveFromUrl(string url);
        Task<IronFile> DownloadAndSaveFromForm(IFormFile url);
        Task<IronFile> GetFileFromFileSystem(string fileName);
        void DeleteFile(string fileName);


    }
}

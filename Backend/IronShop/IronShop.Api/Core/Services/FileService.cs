using IronShop.Api.Core.Entities.Base;
using IronShop.Api.Core.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Services
{
    public class FileService : IFileService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger = Log.ForContext<FileService>();

        public FileService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IronFile> DownloadAndSaveFromUrl(string url)
        {
            IronFile fileResult = new IronFile();

            if (string.IsNullOrEmpty(url))
                return fileResult;

            try
            {
                byte[] fileArray = null;
                using (var client = new HttpClient())
                {
                    using (var result = await client.GetAsync(url))
                        if (result.IsSuccessStatusCode)
                            fileArray = await result.Content.ReadAsByteArrayAsync();
                }

                if (fileArray != null)
                {
                    var fileExtension = Path.GetExtension(url);
                    var pathFolderStorage = _configuration["FolderStorage"];

                    string fileName = Guid.NewGuid().ToString() + fileExtension;

                    var pathAbsolute = Path.Combine(pathFolderStorage, fileName);
                    using (FileStream fs = System.IO.File.Create(pathAbsolute))
                    {
                        await fs.WriteAsync(fileArray, 0, fileArray.Length);
                    }

                    fileResult.Path = pathAbsolute;
                    fileResult.FileName = fileName;
                    fileResult.Success = true;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Download Error. Url { url }");
            }

            return fileResult;
        }

        public async Task<IronFile> DownloadAndSaveFromForm(IFormFile file)
        {
            IronFile result = new IronFile();
            try
            {
                var fileExtension = Path.GetExtension(file.FileName);
                var pathFolderStorage = _configuration["FolderStorage"];
                var newFile = Guid.NewGuid().ToString() + fileExtension;

                var pathAbsolute = Path.Combine(pathFolderStorage, newFile);

                byte[] arrayFile;

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    arrayFile = memoryStream.ToArray();
                }

                using (FileStream fs = System.IO.File.Create(pathAbsolute))
                {
                    await fs.WriteAsync(arrayFile, 0, arrayFile.Length);
                }

                result.Path = pathAbsolute;
                result.FileName = newFile;
                result.Success = true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Download Error from Form");
            }

            return result;
        }

        public void DeleteFile(string fileName)
        {
            try
            {
                var pathFileStorage = _configuration["FolderStorage"];
                var filePath = Path.Combine( pathFileStorage, fileName);

                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error - Delete file {fileName}");
            }
        }

        public async Task<IronFile> GetFileFromFileSystem(string fileName)
        {
            var result = new IronFile();
            try
            {
                var pathFolderStorage = _configuration["FolderStorage"];
                var filePath = Path.Combine(pathFolderStorage, fileName);

                using (MemoryStream fileStream = new MemoryStream())
                {
                    using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                        await file.CopyToAsync(fileStream);
                    result.Metadata = fileStream.ToArray();
                }

                var provider = new FileExtensionContentTypeProvider();
                string contentType;
                if (!provider.TryGetContentType(fileName, out contentType))
                    contentType = "application/octet-stream";

                result.ContentType = contentType;
                result.FileName = fileName;
                result.Path = filePath;
                result.Success = true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error getting file {fileName} from file system");
            }

            return result;
        }
        
    }
}

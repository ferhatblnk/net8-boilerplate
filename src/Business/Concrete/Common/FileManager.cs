using Business.Abstract;
using Core.Settings.Concrete;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;

namespace Business.Concrete
{
    public class FileManager : BaseManager, IFileService
    {
        public Task<string> Save(IFormFile file)
        {
            string path = string.Empty;

            if (file != null && file.Length > 0)
            {
                var timePrefix = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + "_";
                var uploads = Path.Combine(Environment.CurrentDirectory, "wwwroot", "uploads");
                var filePath = Path.Combine(uploads, timePrefix + file.FileName);
                var webpPath = filePath;

                path = Path.Combine("uploads", timePrefix + file.FileName);

                // if (file.Length > 0 && file.ContentType.ToLowerInvariant().Contains("image"))
                // {
                //     webpPath = Path.Combine(uploads, timePrefix + "webp_" + file.FileName);
                //     path = Path.Combine("uploads", timePrefix + "webp_" + file.FileName);
                // }

                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                // if (file.Length > 0 && file.ContentType.ToLowerInvariant().Contains("image"))
                // {
                //     var imageBytes = File.ReadAllBytes(filePath);
                //     using var inStream = new MemoryStream(imageBytes);
                //     using var myImage = Image.Load(inStream);
                //     using var outStream = new FileStream(webpPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                //     myImage.Save(outStream, new WebpEncoder());

                //     inStream.Flush();
                //     outStream.Flush();

                //     File.Delete(filePath);
                // }
            }

            var appSettings = ServiceTool.ServiceProvider.GetService<IOptionsSnapshot<AppSettings>>();

            return Task.FromResult(Path.Combine(appSettings.Value.StorageConfig.FileRootUrl, path).Replace("\\", "/"));
        }
    }
}

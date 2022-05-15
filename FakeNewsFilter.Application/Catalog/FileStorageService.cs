using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace FakeNewsFilter.Application.Common
{
    public interface IFileStorageService
    {
        Task DeleteFileAsync(string fileName);

        string GetFileUrl(string fileName);

        string GetNewsUrl(string alias, int newsId);

        Task SaveFileAsync(Stream mediaBinaryStream, string fileName);
    }

    public class FileStorageService : IFileStorageService
    {
        public static string USER_CONTENT_FOLDER_NAME { get; set; } = "images";

        private readonly string _userContentFolder;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public FileStorageService(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _userContentFolder = Path.Combine(webHostEnvironment.WebRootPath, USER_CONTENT_FOLDER_NAME);
            _httpContextAccessor = httpContextAccessor;
    }

        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }
        
        public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
        }

        //Hàm lấy ra URL hình ảnh (trường hợp ảnh được tạo bởi hệ thống)
        public string GetFileUrl(string fileName)
        {
            return _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host.Value + $"/{USER_CONTENT_FOLDER_NAME}/{fileName}";
        }

        //Hàm lấy ra URL của tin tức (trong trường hợp tin được tạo bởi hệ thống)
        public string GetNewsUrl(string alias, int newsId)
        {
            // return _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host.Value + $"/news/{alias}";
            return _httpContextAccessor.HttpContext.Request.Scheme + "://" + "localhost:5003" + $"/news/{alias}-{newsId}";
        }
    }
}
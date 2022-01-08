using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using elFinder.NetCore;
using elFinder.NetCore.Drivers.FileSystem;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;

namespace FakeNewsFilter.AdminApp.Controllers;

[Authorize]
public class FileManagerController : Controller
{
    IWebHostEnvironment _env;
    public FileManagerController(IWebHostEnvironment env)
    {
        _env = env;
    }

    [Route("/file-manager")]
    public IActionResult Index()
    {
        return View();
    }
    // Url để client-side kết nối đến backend
    // /el-finder-file-system/connector
    [Route("/file-manager-connector")]
    public async Task<IActionResult> Connector()
    {
        var connector = GetConnector();
        return await connector.ProcessAsync(Request);
    }

    // Địa chỉ để truy vấn thumbnail
    // /el-finder-file-system/thumb
    [Route("/file-manager-thumb/{hash}")]
    public async Task<IActionResult> Thumbs(string hash)
    {
        var connector = GetConnector();
        return await connector.GetThumbnailAsync(HttpContext.Request, HttpContext.Response, hash);
    }

    private Connector GetConnector()
    {
        // Thư mục gốc lưu trữ là wwwwroot/files (đảm bảo có tạo thư mục này)
        string pathroot = "img";
        
        var driver = new FileSystemDriver();

        string absoluteUrl = UriHelper.BuildAbsolute(Request.Scheme, Request.Host);
        var uri = new Uri(absoluteUrl);
        string rootDirectory = Path.Combine(_env.WebRootPath, pathroot);

        // .. ... wwww/files
        // string rootDirectory = Path.Combine(baseUrl.AbsoluteUri, pathroot);

        // https://localhost:5001/files/
        // string url = $"{baseUrl.Scheme}://{baseUrl.Authority}/{requestUrl}/";
        // string urlthumb = $"{baseUrl.Scheme}://{baseUrl.Authority}/file-manager-thumb/";

        string url = $"{uri.Scheme}://{uri.Authority}/{pathroot}/";
        string urlthumb = $"{uri.Scheme}://{uri.Authority}/file-manager-thumb/";



        var root = new RootVolume(rootDirectory, url, urlthumb)
        {  
            //IsReadOnly = !User.IsInRole("Administrators")
            IsReadOnly = false, // Can be readonly according to user's membership permission
            IsLocked = false, // If locked, files and directories cannot be deleted, renamed or moved
            Alias = "Files", // Beautiful name given to the root/home folder
            //MaxUploadSizeInKb = 2048, // Limit imposed to user uploaded file <= 2048 KB
            //LockedFolders = new List<string>(new string[] { "Folder1" }
            ThumbnailSize = 100,
        };
        
        driver.AddRoot(root);

        return new Connector(driver)
        {
            // This allows support for the "onlyMimes" option on the client.
            MimeDetect = MimeDetectOption.Internal
        };
    }
}
using System;
using System.IO;
using System.Threading.Tasks;

namespace FakeNewsFilter.Application.Common
{
    public interface IFileStorageService
    {
        Task DeleteFileAsync(string fileName);

        string GetFileUrl(string fileName);

        Task SaveFileAsync(Stream mediaBinaryStream, string fileName);
    }
}
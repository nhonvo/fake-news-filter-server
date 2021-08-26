using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FakeNewsFilter.Application.Common;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Utilities.Exceptions;
using FakeNewsFilter.ViewModel.Catalog.Media;
using Microsoft.AspNetCore.Http;

namespace FakeNewsFilter.Application.Catalog.MediaManager
{
    public class ManageMediaService : IManageMediaService
    {
        private readonly ApplicationDBContext _context;

        private readonly FileStorageService _storageService;

        public ManageMediaService(ApplicationDBContext context, FileStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<int> RemoveMedia(int mediaId)
        {
            var media = await _context.Media.FindAsync(mediaId);
            if (media == null)
                throw new FakeNewsException($"Cannot find an media with id {mediaId}");

            if (media.PathMedia != null)
                await _storageService.DeleteFileAsync(media.PathMedia);

            _context.Media.Remove(media);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateMedia(int mediaId, NewsMediaCreateRequest request)
        {
            var media = await _context.Media.FindAsync(mediaId);
            if (media == null)
                throw new FakeNewsException($"Cannot find an media with id {mediaId}");

            if (request.MediaFile != null)
            {
                media.PathMedia = await this.SaveFile(request.MediaFile);
                media.FileSize = request.MediaFile.Length;
            }

            _context.Media.Update(media);

            return await _context.SaveChangesAsync();
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
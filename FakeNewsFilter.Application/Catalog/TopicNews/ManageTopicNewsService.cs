using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using FakeNewsFilter.Application.Common;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.Utilities.Exceptions;
using FakeNewsFilter.ViewModel.Catalog.TopicNews;
using Microsoft.AspNetCore.Http;

namespace FakeNewsFilter.Application.Catalog.TopicNews
{
    public class ManageTopicNewsService : IManageTopicNewsService
    {
        private readonly ApplicationDBContext _context;

        private  FileStorageService _storageService;

        private readonly IMapper _mapper;

        public ManageTopicNewsService(ApplicationDBContext context, FileStorageService storageService, IMapper mapper)
        {
            _context = context;
            _storageService = storageService;
            _mapper = mapper;
        }

        public async Task<int> Create(TopicNewsCreateRequest request)
        {
            var topic = new Data.Entities.TopicNews()
            {
                Label = request.Label,
                Description = request.Description,
                Tag = request.Tag,
                Timestamp = DateTime.Now
            };
            
                if (request.MediaLink != null)
                {
                    topic.Media = new Media()
                    {
                        Caption = "Thumbnail Image",
                        DateCreated = DateTime.Now,
                        Url = request.MediaLink,
                        Type = request.Type,
                        SortOrder = 1
                    };
                };
                //Save Media
                if (request.ThumbnailMedia != null)
                {
                    
                    topic.Media = new Media()
                    {
                        Caption = "Thumbnail Image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbnailMedia.Length,
                        PathMedia = await this.SaveFile(request.ThumbnailMedia),
                        Type = request.Type,
                        SortOrder = 1
                    };
                }
            
            _context.TopicNews.Add(topic);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(TopicNewsUpdateRequest request)
        {
            var topic = await _context.TopicNews.FindAsync(request.TopicId);

            if(topic == null)
                throw new FakeNewsException($"Cannont find a topic news with Id is: {request.TopicId}");

            topic.Label = request.Label;
            topic.Description = request.Description;
            topic.Timestamp = DateTime.Now;
            
            if (request.ThumbnailMedia != null || request.MediaLink !=null)
            {
                var thumb = _context.Media.FirstOrDefault(i => i.TopicId == request.TopicId);

                thumb.FileSize = 0;

                if (thumb.PathMedia != null)
                {
                    await _storageService.DeleteFileAsync(thumb.PathMedia);
                    thumb.PathMedia = null; 
                }

                if (request.ThumbnailMedia != null)
                {
                    thumb.FileSize = request.ThumbnailMedia.Length;
                    thumb.PathMedia = await SaveFile(request.ThumbnailMedia);
                }

                thumb.Type = request.Type;
                thumb.Url = request.MediaLink;
                
                _context.Media.Update(thumb);
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int TopicId)
        {
            var topic = await _context.TopicNews.FindAsync(TopicId);

            if (topic == null) throw new FakeNewsException($"Cannot find a News with Id: {TopicId}");


            var media = _context.Media.Find(TopicId);

            if (media != null && media.PathMedia != null)
                await _storageService.DeleteFileAsync(media.PathMedia);


            _context.TopicNews.Remove(topic);

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

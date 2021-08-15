
using System.Threading.Tasks;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.Utilities.Exceptions;
using FakeNewsFilter.ViewModel.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Collections.Generic;
using System;
using FakeNewsFilter.Application.Common;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;

namespace FakeNewsFilter.Application.Catalog.NewsManage
{
    public class ManageNewsService : IManageNewsService
    {
        private readonly ApplicationDBContext _context;

        private readonly IStorageService _storageService;

        public ManageNewsService(ApplicationDBContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        //Get All News Paging
        public async Task<PagedResult<NewsViewModel>> GetAllPaging(GetManageNewsPagingRequest request)
        {
            //1. Select Join
            var query = from n in _context.News
                        join nit in _context.NewsInTopics on n.NewsId equals nit.NewsId
                        join c in _context.TopicNews on nit.TopicId equals c.TopicId
                        select new { n, nit, c };

            //2. Filter
            if (request.TopicIds.Count > 0)
            {
                query = query.Where(t => request.TopicIds.Contains(t.nit.TopicId));
            }

            //3. Paging
            int TotalRow = await query.CountAsync();

            var data = await query
                .Skip((request.pageIndex - 1) * request.pageSize)
                .Take(request.pageSize)
                .Select(x => new NewsViewModel()
                {
                    NewsId = x.n.NewsId,
                    Name = x.n.Name,
                    TopicId = x.c.TopicId,
                    LabelTopic = x.c.Label,

                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<NewsViewModel>()
            {
                TotalRecord = TotalRow,
                Items = data
            };

            return pagedResult;
        }

        //Create News
        public async Task<int> Create(NewsCreateRequest request)
        {
            var news = new News()
            {
                NewsId = request.NewsId,

                Name = request.Name,

                Description = request.Description,

                SourceLink = request.SourceLink,
            };
            if(request.MediaLink != null)
            {
                news.Media = new Media()
                {
                    Caption = "Thumbnail Image",
                    DateCreated = DateTime.Now,
                    Url = request.MediaLink,
                    Type = request.Type,
                    SortOrder = 1
                };
            };
            //Save Image
            if (request.ThumbnailImage != null)
            {
                news.Media =  new Media()
                {
                        Caption = "Thumbnail Image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbnailImage.Length,
                        PathMedia = await this.SaveFile(request.ThumbnailImage),
                        Type = request.Type,
                        SortOrder = 1
                };
            }
            _context.News.Add(news);

            return await _context.SaveChangesAsync();

        }

        //Update News
        public async Task<int> Update(NewsUpdateRequest request)
        {
            var news_update = await _context.News.FindAsync(request.Id);

            if (news_update == null) throw new FakeNewsException($"Cannont find a news with Id is: {request.Id}");

            news_update.Name = request.Name;
            news_update.Description = request.Description;
            
            //Save Image
            if (request.ThumbnailMedia != null)
            {
                var thumb = _context.Media.FirstOrDefault(i => i.News.NewsId == request.Id);
                if(thumb!=null)
                {

                    thumb.FileSize = request.ThumbnailMedia.Length;
                    thumb.Url = await this.SaveFile(request.ThumbnailMedia);
                    _context.Media.Update(thumb);
                }
            }

            return await _context.SaveChangesAsync();
        }


        //Update Link News
        public async Task<bool> UpdateLink(int newsId, string newLink)
        {
            var news_update = await _context.News.FindAsync(newsId);

            if (news_update == null) throw new FakeNewsException($"Cannont find a news with Id is: {newsId}");

            news_update.SourceLink = newLink;
          
            return await _context.SaveChangesAsync() > 0;
        }


        //Delete News
        public async Task<int> Delete(int newsId)
        {
            var news = await _context.News.FindAsync(newsId);

            if (news == null) throw new FakeNewsException($"Cannot find a News with Id: {newsId}");


            var images = _context.Media.Where(i => i.News.NewsId == newsId);

            foreach(var image in images)
            {
                await _storageService.DeleteFileAsync(image.PathMedia);
            }

            _context.News.Remove(news);

            return await _context.SaveChangesAsync();
        }

        
        //Save File
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}

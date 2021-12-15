﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.ViewModel.Catalog.ExtraFeatures;
using FakeNewsFilter.ViewModel.Catalog.Media;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using FakeNewsFilter.ViewModel.Catalog.TopicNews;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.EntityFrameworkCore;

namespace FakeNewsFilter.Application.Catalog
{
    public interface IExtraFeaturesService
    {
        Task<ApiResult<SearchViewModel>> SearchContent(SeachContentRequest request);
    }

    public class ExtraFeaturesService : IExtraFeaturesService
    {
        private readonly ApplicationDBContext _context;

        private readonly IMapper _mapper;

        public ExtraFeaturesService(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<ApiResult<SearchViewModel>> SearchContent(SeachContentRequest request)
        {
            var language = await _context.Languages.SingleOrDefaultAsync(x => x.Id == request.languageId);

            var query = from t in _context.TopicNews
                        where ((string.IsNullOrEmpty(request.languageId) || t.LanguageId == request.languageId) && t.Description.Contains(request.keyword))
                        select new
                        {
                            topic = t,
                            newscount = _context.NewsInTopics.Where(n => n.TopicId == t.TopicId).Count(),
                            thumb = _context.Media.Where(m => m.MediaId == t.ThumbTopic).Select(m => m.PathMedia).FirstOrDefault(),
                            synctime = _context.NewsInTopics.Where(n => n.TopicId == t.TopicId).Max(n => n.Timestamp)
                        };

            var list_topics = await query.Select(x => new TopicInfoVM()
            {
                TopicId = x.topic.TopicId,
                Label = x.topic.Label,
                Tag = x.topic.Tag,
                Description = x.topic.Description,
                NONews = x.newscount,
                ThumbImage = x.thumb,
                Status = x.topic.Status,
                LanguageId = x.topic.LanguageId,
                RealTime = x.synctime,
            }).ToListAsync();

            var list_news = await _context.News.Where(n => string.IsNullOrEmpty(request.languageId) ? true : n.LanguageId == request.languageId && n.Description.Contains(request.keyword))
                .Select(x => new NewsViewModel()
                {
                    NewsId = x.NewsId,
                    Name = x.Name,
                    TopicInfo = x.NewsInTopics.Select(o => new TopicInfo { TopicId = o.TopicId, TopicName = o.TopicNews.Tag }).ToList(),
                    Description = x.Description,
                    Content = x.Content,
                    OfficialRating = x.OfficialRating,
                    Publisher = x.Publisher,
                    Status = x.Status,
                    ThumbNews = x.Media.PathMedia,
                    LanguageId = x.LanguageId,
                    Timestamp = x.Timestamp,
                }).ToListAsync();

            if(list_topics.Count == 0 && list_news.Count ==0)
            {
                return new ApiErrorResult<SearchViewModel>("NotFound");
            }

            var finalresult = new SearchViewModel
            {
                TopicNews = list_topics,
                CountTopic = list_topics.Count,
                News = list_news,
                CountNews = list_news.Count
            };


            return new ApiSuccessResult<SearchViewModel>("SearchSuccessful", finalresult);
        }
    }
}


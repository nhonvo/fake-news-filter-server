using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FakeNewsFilter.Data.Enums;
using FakeNewsFilter.Utilities.Exceptions;
using FakeNewsFilter.ViewModel.Catalog;
using FakeNewsFilter.ViewModel.Catalog.NewsManage;
using FakeNewsFilter.ViewModel.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FakeNewsFilter.Application.Catalog;

public interface ICloneNewsService
{
    Task<ApiResult<List<NewsOutSourceCreateRequest>>> GetOigetitCategory(int categoryId, int topicId);
    Task<string> GetOigetitNewsDesc(string newsId);
}

public class CloneNewsService : ICloneNewsService
{
    public async Task<ApiResult<List<NewsOutSourceCreateRequest>>> GetOigetitCategory(int categoryId, int topicId)
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://api.oigetit.com:8081/V2/GetCategoryNews/EN/" + categoryId);
            request.Method = HttpMethod.Get;
            HttpResponseMessage response = await httpClient.SendAsync(request);
            var body = await response.Content.ReadAsStringAsync();
            var statusCode = response.StatusCode;

            var oigetitNewsList = JsonConvert.DeserializeObject<List<OigetitNews>>(body);
            
            var newsOutSourceCreateRequests = new List<NewsOutSourceCreateRequest>();
            foreach (var oigetitNews in oigetitNewsList)
            {
                var oigetitDesc = await GetOigetitNewsDesc(oigetitNews.ID.ToString());

                var newsOutSourceCreateRequest = new NewsOutSourceCreateRequest()
                {
                    Title = oigetitNews.Title,
                    Description = oigetitDesc,
                    OfficialRating = LabelNews.undefined.ToString(),
                    UrlNews = oigetitNews.URL,
                    ImageLink = oigetitNews.ImageURL,
                    LanguageId = "en",
                    TopicId = new List<int>() {topicId},
                    Publisher = oigetitNews.Feed,
                    DatePublished = DateTime.Parse(oigetitNews.PubDate),
                    isVote = true,
                    SourceCreate = SourceCreate.Oigetit.ToString()
                };

                newsOutSourceCreateRequests.Add(newsOutSourceCreateRequest);
            }

            if (statusCode == HttpStatusCode.OK)
            {
                return new ApiSuccessResult<List<NewsOutSourceCreateRequest>>("Get Oigetit Category Success",
                    newsOutSourceCreateRequests);
            }

            return new ApiErrorResult<List<NewsOutSourceCreateRequest>>((int) statusCode,
                "Get Oigetit Category Failed");
        }

        catch (FakeNewsException e)
        {
            return new ApiErrorResult<List<NewsOutSourceCreateRequest>>(400, "Get Oigetit Category Failed");
        }
    }

    public async Task<string> GetOigetitNewsDesc(string newsId)
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();

            request.RequestUri = new Uri("https://api.oigetit.com:8081/V2/GetArticle/" + newsId);
            request.Method = HttpMethod.Get;

            HttpResponseMessage response = await httpClient.SendAsync(request);

            var body = await response.Content.ReadAsStringAsync();
            var statusCode = response.StatusCode;

            JObject result = JObject.Parse(body);

            if (statusCode == HttpStatusCode.OK)
            {
                return result["Description"]!.ToString();
            }

            return "An error has occurred while trying to get the news description";
        }
        catch (FakeNewsException e)
        {
            return "An error has occurred while trying to get the news description";
        }
    }
}
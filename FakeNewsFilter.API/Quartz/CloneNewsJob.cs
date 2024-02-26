using System.Threading.Tasks;
using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.Utilities.Constants;
using Quartz;


namespace FakeNewsFilter.Quartz;

public class CloneNewsJob 

{
    private readonly INewsService _newsService;
    private readonly ICloneNewsService _cloneNewsService;

    public CloneNewsJob(INewsService newsService, ICloneNewsService cloneNewsService)
    {
        _newsService = newsService;
        _cloneNewsService = cloneNewsService;
    }

    // public async Task Execute(IJobExecutionContext context)
    // {
    //     foreach (var topic in SystemConstants.MapOigetitTopic)
    //     {
    //         var sysTopicId = SystemConstants.MapSysTopics[topic.Key];
            
    //         var news = await _cloneNewsService.GetOigetitCategory(topic.Value, sysTopicId);

    //         await _newsService.CreateBatchNews(news.ResultObj, sysTopicId);
    //     }
    // }
}